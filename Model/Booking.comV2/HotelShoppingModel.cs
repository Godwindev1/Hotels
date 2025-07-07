using AutoMapper;
using Hotel.Amadeus;
using Hotel.Data.Amadeus.data.shopping;

using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Dto.Amadeus.dto.offers.dto;
using Hotel.Logging;

using Hotel.SyncServices;
using Hotel.Utility;

namespace Hotel.model
{
    public partial class HotelShoppingModel
    {
        private KeyBasedLogging _logger;
        private ErrorType classInformation;


        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
    }
}
