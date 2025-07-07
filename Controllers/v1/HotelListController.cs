using Asp.Versioning;
using AutoMapper;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Logging;
using Hotel.Model.AmadeusV1;
using Hotel.SyncServices;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:ApiVersion}")]
    public class HotelListController : Controller
    {
        ErrorType Information;

        private readonly HttpClientImplementation clientImplementation;
        private HotelModel _model;
        private KeyBasedLogging _logger;
        public HotelListController(IHttpClient client, IMapper mapper, KeyBasedLogging loging)
        {
            clientImplementation = (HttpClientImplementation)client;
            _model = new HotelModel(client, mapper, loging, out Information);

            _logger = loging;
        }

        [HttpGet("hotel/list/by-hotel/{HotelID}")]
        public async Task<ActionResult<HotelListReadDto>> GetHotelByID(string HotelID)
        {
            try
            {
                var Results = (HotelListReadDto)await _model.HotelByHotelID(HotelID);

                if (Results != null)
                {
                    return new ActionResult<HotelListReadDto>(Results);
                }
                else
                {
                    var message = _logger.RecieveMessage(Information.Key);

                    return StatusCode(500, message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Server Error ");
            }
        }



        [HttpGet("Hotel/List/{lat}/{longitude}")]
        public async Task<ActionResult<List<HotelListReadDto>>> GetHotelListGeocode(string lat, string longitude, int radius = 1, string Unit = "KM", [FromQuery] List<string> amenities = null, [FromQuery] List<string> ratings = null)
        {
            try
            {
                var Results = (List<HotelListReadDto>)await _model.ListHotelsByGeoCode(lat, longitude, radius, Unit, amenities, ratings);

                if (Results != null)
                {
                    return new ActionResult<List<HotelListReadDto>>(Results);
                }
                else
                {
                    var message = _logger.RecieveMessage(Information.Key);

                    return StatusCode(500, message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Server Error ");
            }
        }

        [HttpGet("Hotel/List/{IataCityCode}")]
        public async Task<ActionResult<List<HotelListReadDto>>> GetHotelList(string IataCityCode, int radius = 1, string Unit = "KM", [FromQuery]List<string> amenities = null, [FromQuery] List<string> ratings = null)
        {
            try
            {
                var Results = (List<HotelListReadDto>)await _model.ListHotelsByCityCode(IataCityCode, radius, Unit, amenities, ratings);

                if (Results != null)
                {
                    return new ActionResult<List<HotelListReadDto>>(Results);
                }
                else
                {
                    return StatusCode(500, $"{_logger.RecieveMessage(Information.Key)}");
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
