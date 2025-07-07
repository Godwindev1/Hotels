using AutoMapper;
using Hotel.Amadeus;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Dto.Amadeus.dto.ratings.dto;
using Hotel.Logging;
using Hotel.SyncServices;
using Hotel.Utility;

namespace Hotel.Model
{
    public partial class HotelSentimentsModel
    {
        private ErrorType classInformation;
        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
        private KeyBasedLogging _logger;
       
    }
}
