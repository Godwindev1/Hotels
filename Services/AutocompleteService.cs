using AutoMapper;
using Grpc.Core;
using Hotel.Logging;
using Hotel.model;
using Hotel.Model;
using Hotel.SyncServices;
using Hotel.Utility;
using Hotels;

namespace Hotel.Services
{
    public class AutocompleteService : AutocompleteService_proto.AutocompleteService_protoBase
    {
        ErrorType Information;

        AutoCompleteModel _model;
        IMapper mapper;
        private KeyBasedLogging _logger;

        public AutocompleteService(IHttpClient client, IMapper mapper, KeyBasedLogging loging)
        {
            this.mapper = mapper;
            _model = new AutoCompleteModel(client, mapper, loging, out Information);
            _logger = loging;
        }


        public override async Task< GRPCAutoCompleteReadDto > CompleteName(AutocomleteRequestData Data, ServerCallContext context)
        {
            var Suggestions = await _model.CompleteName(Data.Keyword, Data.SubType, Data.CountryCode, Data.Lang, Data.Max);

            if (Suggestions == null)
            {
                var message = _logger.RecieveMessage(Information.Key);
                Console.WriteLine(message);


                var trailers = new Metadata
                {
                    { "error-code", "INTERNAL_SERVER_ERROR" },
                    { "error-info", $"{message}" }
                };

                throw new RpcException(new Status(StatusCode.Cancelled, "See Error Info "), trailers);
            }


            return mapper.Map<Hotels.GRPCAutoCompleteReadDto>(Suggestions);

        }
    }
}
