using AutoMapper;
using Hotel.Amadeus;
using Hotel.Data.Amadeus.data.Autocomplete;
using Hotel.Dto.Amadeus.dto.autocomplete.dto;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Dto.Amadeus.dto.offers.dto;
using Hotel.Logging;
using Hotel.SyncServices;
using Hotel.Utility;
using System.ComponentModel;

namespace Hotel.Model.AmadeusV1
{
    public partial class  AutoCompleteModel
    {
        private ErrorType classInformation;
        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
        private KeyBasedLogging _logger;
        public AutoCompleteModel(IHttpClient client, IMapper mapper, KeyBasedLogging logger, out ErrorType info)
        {
            _httpClient = (HttpClientImplementation)client;
            Automapper = (Mapper)mapper;

            _logger = logger;

            classInformation.ServiceName = "AutoCompletModel.cs";
            classInformation.Key = "AutoCompletModel";

            info = classInformation;
        }


        private async Task<string> ForwardRequest(HttpMessageHeaders URLqueryParameters)
        {
            var HttpRequest = new Request(Constants.GET, AmadeusHotelConstants.AutoComplete,
                 URLqueryParameters, null, Constants.BASEURL);

            var ResponseJSON = await _httpClient.SendHttpRequest(Request.ConvertToHttpRequestMessage(HttpRequest));

            return ResponseJSON;
        }


        private async Task<List<AutoCompleteReadDto>> MapDataToDto(string ResponseJson)
        {
            MessageStructuring Logmessages = new MessageStructuring();
            Logmessages.AddMessage("MapDataToDto");

            var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<AutoCompleteRootList>(ResponseJson);

            if (Result.data == null)
            {
                Logmessages.AddJson(" Request Error ", ResponseJson);

                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.ERROR, classInformation);
                return null;
            }


            var MappingResults = Automapper.Map<List<AutoCompleteReadDto>>(Result.data);

            if(MappingResults == null)
            {
                Logmessages.AddMessage(" Error While Mapping to AutoCompleteReadDto list");
                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.ERROR, classInformation);
                 return null;
            }

            return MappingResults;
        }


        public async Task<List<AutoCompleteReadDto>> CompleteName(string keyword, string subType, string countryCode = "FR", string lang = "EN", int Max = 5 )
        {
            MessageStructuring LogMessages = new MessageStructuring();
            LogMessages.AddMessage("CompleteName");


           if(! HelperMethods.TestBearerToken(ref LogMessages, ref _logger, classInformation))
            {
                return null;
            }

            try
            {
                HttpMessageHeaders URLqueryParameters = new HttpMessageHeaders(AmadeusHotelConstants.keyword, keyword);
                URLqueryParameters.Add(AmadeusHotelConstants.subType, subType);
                URLqueryParameters.Add(AmadeusHotelConstants.countryCode, countryCode);
                URLqueryParameters.Add(AmadeusHotelConstants.lang, lang);
                URLqueryParameters.Add(AmadeusHotelConstants.max, $"{Max}");


                var ResponseJSON = await ForwardRequest(URLqueryParameters);


                _logger.LogMessage(LogMessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                return await MapDataToDto(ResponseJSON);
            }
            catch (Exception ex)
            {
                HelperMethods.LogExceptionMessages(ref _logger, ref LogMessages, ex, classInformation);

                return null;
            }



        }

    }
}
