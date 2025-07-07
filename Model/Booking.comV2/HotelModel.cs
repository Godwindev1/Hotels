using AutoMapper;
using Hotel.Amadeus;
using Hotel.Data.Amadeus.data.List;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Logging;
using Hotel.SyncServices;
using Hotel.Utility;

namespace Hotel.model
{
    public partial class HotelModel : ParseJsonLists
    {
        private ErrorType classInformation;
        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
        private KeyBasedLogging _logger;
     
    }
}
