using AutoMapper;
using Hotel.Dto.autocomplete.dto;
using Hotel.Logging;
using Hotel.Model;
using Hotel.SyncServices;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [ApiController]
    public class AutoCompleteController : Controller
    {


        AutoCompleteModel _AutoCompleteModel;
        ErrorType Information;

        private readonly HttpClientImplementation clientImplementation;
        private KeyBasedLogging _logger;
        public AutoCompleteController(IHttpClient client, IMapper mapper, KeyBasedLogging loging)
        {
            _AutoCompleteModel = new AutoCompleteModel(client, mapper, loging, out Information);
            this.clientImplementation = (HttpClientImplementation)client;

            _logger = loging;
        }


        [HttpGet("/hotel/locations/completename")]
        public async Task<ActionResult<List<AutoCompleteReadDto>>> CompleteName(string keyword, string subType, string countryCode = "FR", string lang = "EN", int Max = 15)
        {
            if(string.IsNullOrWhiteSpace(keyword) || string.IsNullOrWhiteSpace(subType))
            {
                return BadRequest("Keyword and SubType Are Required Parameters, { subtype: // one of \n ->HOTEL_GDS \n ->HOTEL_LEISURE}");
            }

            try
            {
                var NameCompletedHotels = await _AutoCompleteModel.CompleteName(keyword, subType, countryCode, lang, Max);

                if (NameCompletedHotels != null)
                {
                    return Ok(NameCompletedHotels);
                }

                else
                {
                    return StatusCode(500, $"{_logger.RecieveMessage(Information.Key)}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server Error: {ex.Message} ");
            }
        }




    }
}
