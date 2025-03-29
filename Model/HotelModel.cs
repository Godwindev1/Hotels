using AutoMapper;
using Hotel.Amadeus;
using Hotel.Data.List;
using Hotel.Dto.List.dto;
using Hotel.Logging;
using Hotel.SyncServices;
using Hotel.Utility;

namespace Hotel.model
{
    public class HotelModel : ParseJsonLists
    {
        private ErrorType classInformation;
        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
        private KeyBasedLogging _logger;
        public HotelModel(IHttpClient client, IMapper mapper, KeyBasedLogging logger, out ErrorType info) {
            _httpClient = (HttpClientImplementation)client;
            Automapper = (Mapper)mapper;

            _logger = logger;

            classInformation.ServiceName = "HotelModel.cs";
            classInformation.Key = "HotelModels";

            info = classInformation;
        }



        public async Task<HotelListReadDto> HotelByHotelID(string hotelID)
        {
            MessageStructuring Logmessages = new MessageStructuring();
            Logmessages.AddMessage("HotelByHotelID\"");

            if (!HelperMethods.TestBearerToken(ref Logmessages, ref _logger, classInformation))
            {
                return null;
            }


            try
            {
                HttpMessageHeaders URLqueryParameters = new HttpMessageHeaders(AmadeusHotelConstants.HotelId, hotelID);
               
            
                var HttpRequest = new Request(Constants.GET, AmadeusHotelConstants.HotelByID,
                  URLqueryParameters, null, Constants.BASEURL);

                var ResponseJSON = await _httpClient.SendHttpRequest(Request.ConvertToHttpRequestMessage(HttpRequest));


                var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<HotelList>(ResponseJSON);

                if (Result.data == null)
                {
                    Logmessages.AddJson("Request Error:", ResponseJSON);
                    _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                    return null;
                }

                var MappingResults = Automapper.Map<HotelListReadDto>(Result.data.FirstOrDefault());


                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                return (HotelListReadDto)MappingResults;
            }

            catch (Exception ex)
            {
                HelperMethods.LogExceptionMessages(ref _logger, ref Logmessages, ex, classInformation);

                return null;
            }


        }

        public async Task<IEnumerable<HotelListReadDto>> ListHotelsByGeoCode(string? Lat, string? longitude, int radius = 1, string unit = "KM", List<string> Amentities = null, List<string> Ratings = null)
        {
            MessageStructuring Logmessages = new MessageStructuring();
            Logmessages.AddMessage("ListHotelsByGeoCode\"");


            if (!HelperMethods.TestBearerToken(ref Logmessages, ref _logger, classInformation))
            {
                return null;
            }

            try
            {
                HttpMessageHeaders URLqueryParameters = new HttpMessageHeaders(AmadeusHotelConstants.Latitude, Lat);
                URLqueryParameters.Add(AmadeusHotelConstants.Longitude, $"{longitude}");
                URLqueryParameters.Add(AmadeusHotelConstants.Radius, $"{radius}");
                URLqueryParameters.Add(AmadeusHotelConstants.RadiusUnit, $"{unit}");

                if (Amentities != null)
                {
                    URLqueryParameters.Add(AmadeusHotelConstants.Amenities, ParseListAsJsonText(Amentities));
                }
                if (Ratings != null)
                {
                    URLqueryParameters.Add(AmadeusHotelConstants.Ratings, ParseListAsJsonText(Ratings));
                }

                var HttpRequest = new Request(Constants.GET, AmadeusHotelConstants.HotelByGeoCode,
                  URLqueryParameters, null, Constants.BASEURL);

                var ResponseJSON = await _httpClient.SendHttpRequest(Request.ConvertToHttpRequestMessage(HttpRequest));


                var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<HotelList>(ResponseJSON);

                if (Result.data == null)
                {
                    Logmessages.AddJson(" Request Error:", ResponseJSON);
                    _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                    return null;
                }

                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);

                var MappingResults = Automapper.Map<List<HotelListReadDto>>(Result.data);
                return (IEnumerable<HotelListReadDto>)MappingResults;
            }

            catch (Exception ex)
            {
                HelperMethods.LogExceptionMessages(ref _logger, ref Logmessages, ex, classInformation);

                return null;
            }

        }



        public async Task<IEnumerable<HotelListReadDto>> ListHotelsByCityCode(string? IATACityCode, int radius = 1, string unit = "KM", List<string> Amentities = null, List<string>  Ratings = null)
        {
            MessageStructuring Logmessages = new MessageStructuring();

            Logmessages.AddMessage("ListHotelsByCityCode\"");

            if (!HelperMethods.TestBearerToken(ref Logmessages, ref _logger, classInformation))
            {
                return null;
            }

            try
            {
                HttpMessageHeaders URLqueryParameters = new HttpMessageHeaders(AmadeusHotelConstants.CityCode, IATACityCode);
                URLqueryParameters.Add(AmadeusHotelConstants.Radius, $"{radius}");
                URLqueryParameters.Add(AmadeusHotelConstants.RadiusUnit, $"{unit}");

                if(Amentities != null) 
                { 
                    URLqueryParameters.Add(AmadeusHotelConstants.Amenities, ParseListAsJsonText(Amentities));
                }
                if (Ratings != null)
                {
                    URLqueryParameters.Add(AmadeusHotelConstants.Ratings, ParseListAsJsonText(Ratings));
                }

                var HttpRequest = new Request(Constants.GET, AmadeusHotelConstants.HotelByCityURL,
                  URLqueryParameters, null, Constants.BASEURL);

                var ResponseJSON = await _httpClient.SendHttpRequest(Request.ConvertToHttpRequestMessage(HttpRequest));


                var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<HotelList>(ResponseJSON);

                if(Result.data  == null)
                {
                    Logmessages.AddJson(" Request Error:", ResponseJSON);
                    _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                    return null;
                }

                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);

                var MappingResults = Automapper.Map<List<HotelListReadDto>>(Result.data);
                return (IEnumerable<HotelListReadDto>)MappingResults;
            }
            catch (Exception ex)
            {

                HelperMethods.LogExceptionMessages(ref _logger, ref Logmessages, ex, classInformation);

                return null;    
            }

        }



    }
}
