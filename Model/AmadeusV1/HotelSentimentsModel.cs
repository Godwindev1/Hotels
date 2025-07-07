using AutoMapper;
using Hotel.Amadeus;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Dto.Amadeus.dto.ratings.dto;
using Hotel.Logging;
using Hotel.SyncServices;
using Hotel.Utility;

namespace Hotel.Model.AmadeusV1
{
    public partial class HotelSentimentsModel
    {
        private ErrorType classInformation;
        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
        private KeyBasedLogging _logger;
        public HotelSentimentsModel(IHttpClient client, IMapper mapper, KeyBasedLogging logger, out ErrorType info) 
        {
            _httpClient = (HttpClientImplementation)client;
            Automapper = (Mapper)mapper;

            _logger = logger;

            classInformation.ServiceName = "HotelSentimentsModel.cs";
            classInformation.Key = "HotelSentimentsModel";

            info = classInformation;
        }



        public async Task<List<HotelSentimentsReadDto>> GetHotelSentiments(string CommaSeperatedIDs)
        {
            MessageStructuring Logmessages = new MessageStructuring();

            if (!HelperMethods.TestBearerToken(ref Logmessages, ref _logger, classInformation))
            {
                return null;
            }

            try
            {
                HttpMessageHeaders URLqueryParameters = new HttpMessageHeaders(AmadeusHotelConstants.HotelId, CommaSeperatedIDs);


                var HttpRequest = new Request(Constants.GET, AmadeusHotelConstants.HotelSentiments,
                  URLqueryParameters, null, Constants.BASEURL);

                var ResponseJSON = await _httpClient.SendHttpRequest(Request.ConvertToHttpRequestMessage(HttpRequest));


                var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<RatingRoot>(ResponseJSON);

                if (Result.data == null)
                {
                    Logmessages.AddJson("Request Error:", ResponseJSON);
                    _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                    return null;
                }

                var MappingResults = Automapper.Map<List<HotelSentimentsReadDto>>(Result.data);

                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                return MappingResults;
            }

            catch (Exception ex)
            {
                HelperMethods.LogExceptionMessages(ref _logger, ref Logmessages, ex, classInformation);

                return null;
            }

        }
    }
}
