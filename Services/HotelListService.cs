using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;
using System.Reflection.Metadata;

using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels;
using Hotel.model;
using AutoMapper;
using Hotel.Logging;
using Hotel.SyncServices;
using Google.Protobuf.Collections;

namespace Hotel.Services
{
    public class HotelListService : HotelListService_proto.HotelListService_protoBase
    {
        ErrorType Information;

        HotelModel _model;
        IMapper mapper;
        private KeyBasedLogging _logger;

        public HotelListService(IHttpClient client, IMapper mapper, KeyBasedLogging loging)
        {
            this.mapper = mapper;
             _model = new HotelModel(client, mapper, loging, out Information);
            _logger = loging;
        }

        public override async  Task<GRPCHotelListReadDto> GetHotelByID(HotelID ID, ServerCallContext context)
        {
            var Hotel = await _model.HotelByHotelID(ID.HotelID_);

            if (Hotel == null)
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


            return mapper.Map<Hotels.GRPCHotelListReadDto>(Hotel);
        }

        
    
        public override async Task<HotelListResponse> ListHotelsByGeoCode(HotelGeoRequest RequestParameters, ServerCallContext context)
        {
          var Hotels = await _model.ListHotelsByGeoCode(RequestParameters.Lat, RequestParameters.Longitude, RequestParameters.Radius, unit: RequestParameters.Unit
                                , Amentities: RequestParameters.Amenities.ToList(), Ratings: RequestParameters.Ratings.ToList());

            if (Hotels == null)
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



            HotelListResponse response = new HotelListResponse();

            var List = mapper.Map<RepeatedField<GRPCHotelListReadDto>>(Hotels);

            response.Hotels.AddRange(List);

            return response;

        }


        public override async Task<HotelListResponse> ListHotelsByCityCode(HotelCityRequest RequestParameters, ServerCallContext context)
        {

            var Hotels = await _model.ListHotelsByCityCode(RequestParameters.IATACityCode, RequestParameters.Radius, RequestParameters.Unit, RequestParameters.Amentities.ToList(), RequestParameters.Ratings.ToList());


            if (Hotels == null)
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

            HotelListResponse response = new HotelListResponse();

            var List = mapper.Map<RepeatedField<GRPCHotelListReadDto>>(Hotels);

            response.Hotels.AddRange(List);

            return response;

        }
    }
}
