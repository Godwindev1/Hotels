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

namespace Hotel.Model
{
    public partial class AutoCompleteModel
    {
        private ErrorType classInformation;
        private readonly HttpClientImplementation _httpClient;
        private readonly Mapper Automapper;
        private KeyBasedLogging _logger;
     
    }
}
