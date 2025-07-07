using AutoMapper;
using Hotel.Dto.Amadeus.dto.offers.dto;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Logging;
using Hotel.SyncServices;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Hotel.Model.AmadeusV1;

namespace Hotel.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:ApiVersion}")]
    public class ShoppingHotelsController : Controller
    {
        private readonly HttpClientImplementation clientImplementation;
        private HotelShoppingModel _model;
        private readonly KeyBasedLogging _logger;
        private readonly ErrorType information;
        public ShoppingHotelsController(IHttpClient clientImplementation, IMapper mapper, KeyBasedLogging logger)
        {
            _model = new HotelShoppingModel(clientImplementation, mapper, logger, out information);
            this.clientImplementation = (HttpClientImplementation)clientImplementation;

            _logger = logger;
        }

        [HttpGet("hotel/shopping")]
        public async Task<ActionResult<HotelOfferReadDtoRoot>> GetHotelOffers(string hotelIds, string checkInDate = "2025-04-10", string checkOutDate = "2025-04-14", string priceRange = "100 - 200", string currency = "USD",
            string paymentPolicy = "NONE", string boardType = "ROOM_ONLY", int adults = 1, int roomQuantity = 1, bool includeClosed = true, bool bestRateOnly = false)
        {
            try
            {
                var Results = (HotelOfferReadDtoRoot)await _model.ShopHotelOffers(hotelIds, checkInDate, checkOutDate, priceRange, currency, paymentPolicy, boardType, adults, roomQuantity, includeClosed, bestRateOnly);

                if (Results != null)
                {
                    return new ActionResult<HotelOfferReadDtoRoot>(Results);
                }
                else
                {
                    return StatusCode(500, $"{_logger.RecieveMessage(information.Key)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Server Error ");
            }
        }

        [HttpGet("hotel/shopping/{offerID}")]
        public async Task<ActionResult<HotelOfferReadDtoRoot>> GetOffer(string offerID)
        {
            try
            {
                var Results = (HotelOfferReadDtoRoot)await _model.GetOfferPrice(offerID);

                if (Results != null)
                {
                    return new ActionResult<HotelOfferReadDtoRoot>(Results);
                }
                else
                {
                    return StatusCode(500, $"{_logger.RecieveMessage(information.Key)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Server Error ");
            }
        }

    }
}
