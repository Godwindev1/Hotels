using AutoMapper;
using Hotel.Dto.Amadeus.dto.ratings.dto;
using Hotel.Logging;
using Hotel.model;
using Hotel.Model.AmadeusV1;
using Hotel.SyncServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using Asp.Versioning;

namespace Hotel.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:ApiVersion}")]
    public class HotelSentimentController : Controller
    {
        HotelSentimentsModel _HotelSentimentsModel;
        ErrorType Information;

        private readonly HttpClientImplementation clientImplementation;
        private KeyBasedLogging _logger;
        public HotelSentimentController(IHttpClient client, IMapper mapper, KeyBasedLogging loging)
        {
            _HotelSentimentsModel = new HotelSentimentsModel(client, mapper, loging, out Information);
            this.clientImplementation = (HttpClientImplementation)client;

            _logger = loging;
        }


        //HotelIDS can Be a list Of MAX-3 comma Seprated hotel IDS.
        [ApiVersion(1.0)]
        [HttpGet("hotel/reputation/Sentiment/{HotelIDs}")]
        public async Task<ActionResult<List<HotelSentimentsReadDto>>> GetHotelRatings(string HotelIDs)
        {
            try
            {
                if(HotelIDs == null)
                {
                    throw new ArgumentException("Argument HotelID Should Not Be Null ");
                }

                var Results = await _HotelSentimentsModel.GetHotelSentiments(HotelIDs);

                if (Results != null)
                {
                    return new ActionResult<List<HotelSentimentsReadDto>>(Results);
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

        [HttpGet("hotel/reputation/Sentiment/String")]
        public string JustAnExtra()
        {
            return "Nice Api EHH";
        }
    }
}
