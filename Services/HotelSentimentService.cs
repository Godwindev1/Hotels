﻿using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using Hotel.Logging;
using Hotel.model;
using Hotel.Model.AmadeusV1;
using Hotel.Services.data;
using Hotel.SyncServices;
using Hotel.Utility;
using Hotels;
using static Hotels.GRPCListOfHotelSentiments.Types;

namespace Hotel.Services
{
    public class HotelSentimentService : GRPCHotelSentimentsService.GRPCHotelSentimentsServiceBase
    {
        ErrorType Information;

        HotelSentimentsModel _model;
        IMapper mapper;
        private KeyBasedLogging _logger;

        public HotelSentimentService(IHttpClient client, IMapper mapper, KeyBasedLogging loging) 
        {
            this.mapper = mapper;
            _model = new HotelSentimentsModel(client, mapper, loging, out Information);
            _logger = loging;
        }


        public override async Task<GRPCListOfHotelSentiments> GetHotelSentiments(Hotels.SentimentsRequest RequestData, ServerCallContext context)
        {
            var results = await _model.GetHotelSentiments(RequestData.CommaSeperatedIDs);

            try
            {
                if (results == null || results.Count == 0)
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
                else
                {
                    GRPCListOfHotelSentiments MappedResult = new GRPCListOfHotelSentiments();
                    MappedResult.HotelSentiments.AddRange(mapper.Map<RepeatedField<GRPCHotelSentimentsReadDto>>(results));

                    return MappedResult;
                }
            }
            catch (RpcException ex)
            {
                QueueMessage Struct = new QueueMessage();
                Struct.AddMessage(ex.Trailers.GetValue("error-code") + " " + ex.Trailers.GetValue("error-info"));


                await MessageQueueService.SendMessage(Struct);
            }


            return null;
        

        }

    }
}
