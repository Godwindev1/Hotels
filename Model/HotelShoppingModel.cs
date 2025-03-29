using AutoMapper;
using Hotel.Amadeus;
using Hotel.Data.List;
using Hotel.Dto.List.dto;
using Hotel.Dto.offers.dto;
using Hotel.Logging;
using Hotel.Resource.HotelList;
using Hotel.Resource.HotelOffers;
using Hotel.SyncServices;
using Hotel.Utility;

namespace Hotel.model
{
    public class HotelShoppingModel
    {
        private KeyBasedLogging _logger;
        private ErrorType classInformation;


        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
        public HotelShoppingModel(IHttpClient client, IMapper mapper, KeyBasedLogging logger, out ErrorType info)
        {
            _httpClient = (HttpClientImplementation)client;
            Automapper = (Mapper)mapper;

            _logger = logger;

            classInformation.ServiceName = "HotelShoppingModel.cs";
            classInformation.Key = "HotelShoppingModel";

            info = classInformation;
        }


        public async Task<HotelOfferReadDtoRoot> GetOfferPrice(string offerID)
        {
            MessageStructuring Logmessages = new MessageStructuring();
            Logmessages.AddMessage("GetOfferPrice");


            if (!HelperMethods.TestBearerToken(ref Logmessages, ref _logger, classInformation))
            {
                return null;
            }

            try
            { 

                HttpMessageHeaders URLqueryParameters = new HttpMessageHeaders(AmadeusHotelConstants.offerId, $"{offerID}");

                var ResponseJSON =  await ForwardRequest(URLqueryParameters);

                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);

                return await MapDataToOffersDto(ResponseJSON);
            }
            catch(Exception ex )
            {
                Logmessages.AddMessage(ex.Message);
                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                
                return null;
            }

        }

        public  async Task<HotelOfferReadDtoRoot>  ShopHotelOffers(string hotelIds, string checkInDate, string checkOutDate, string priceRange, string currency,
            string paymentPolicy = "NONE", string boardType = "ROOM_ONLY",  int adults = 1,  int roomQuantity = 1, bool includeClosed = true, bool bestRateOnly = false)
        {
            MessageStructuring Logmessages = new MessageStructuring();
            Logmessages.AddMessage("ShopHotelOffers");


            if (!HelperMethods.TestBearerToken(ref Logmessages, ref _logger, classInformation))
            {
                return null;
            }

            try
            {
                HttpMessageHeaders URLqueryParameters = new HttpMessageHeaders(AmadeusHotelConstants.HotelId, hotelIds);
                URLqueryParameters.Add(AmadeusHotelConstants.Adults, $"{adults}");
                URLqueryParameters.Add(AmadeusHotelConstants.CheckInDate, $"{checkInDate}");
                URLqueryParameters.Add(AmadeusHotelConstants.checkOutDate, $"{checkOutDate}");
                URLqueryParameters.Add(AmadeusHotelConstants.RoomQuantity, $"{roomQuantity}");
                URLqueryParameters.Add(AmadeusHotelConstants.priceRange, $"{priceRange}");
                URLqueryParameters.Add(AmadeusHotelConstants.currency, $"{currency}");
                URLqueryParameters.Add(AmadeusHotelConstants.paymentPolicy, $"{paymentPolicy}");
                URLqueryParameters.Add(AmadeusHotelConstants.boardType, $"{boardType}");
                URLqueryParameters.Add(AmadeusHotelConstants.includeClosed, $"{includeClosed}");
                URLqueryParameters.Add(AmadeusHotelConstants.bestRateOnly, $"{bestRateOnly}");


                var ResponseJSON = await ForwardRequest(URLqueryParameters);

                Console.WriteLine(ResponseJSON);

                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
                return await MapDataToOffersDto(ResponseJSON);
            }
            catch (Exception ex)
            {
                HelperMethods.LogExceptionMessages(ref _logger, ref Logmessages, ex, classInformation);

                return null;
            }


        }



        private async Task<string> ForwardRequest(HttpMessageHeaders URLqueryParameters)
        {
            var HttpRequest = new Request(Constants.GET, AmadeusHotelConstants.ShoppingOffersHotelID,
                 URLqueryParameters, null, Constants.BASEURL);

            var ResponseJSON = await _httpClient.SendHttpRequest(Request.ConvertToHttpRequestMessage(HttpRequest));

            return ResponseJSON;
        }


        private async Task<HotelOfferReadDtoRoot> MapDataToOffersDto(string ResponseJson)
        {
            var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<HotelOfferRoot>(ResponseJson);

            if (Result.data == null)
            {
                MessageStructuring Logmessages = new MessageStructuring();
                Logmessages.AddMessage("GetOfferPrice");
                Logmessages.AddJson(" Request Error", ResponseJson);
              
                _logger.LogMessage(Logmessages.GetMessage(), classInformation.Key, Severity.INFO, classInformation);
            }


            var MappingResults = Automapper.Map<List<HotelOfferReadDto>>(Result.data.offers);

            HotelOfferReadDtoRoot HotelOfferRootDto = new HotelOfferReadDtoRoot();
            HotelOfferRootDto.Available = Result.data.available;
            HotelOfferRootDto.hotel = Automapper.Map<HotelListReadDto>(Result.data.hotel);
            HotelOfferRootDto.data = MappingResults;

            return HotelOfferRootDto;
        }


    }
}
