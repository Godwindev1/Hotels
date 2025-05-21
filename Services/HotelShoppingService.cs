using AutoMapper;
using Grpc.Core;
using Hotel.Logging;
using Hotel.model;
using Hotel.Services.data;
using Hotel.SyncServices;
using Hotel.Utility;
using Hotels;

namespace Hotel.Services
{
    public class HotelShoppingService : Hotels.Shopping_proto.Shopping_protoBase
    {
        ErrorType Information;

        HotelShoppingModel _model;
        IMapper mapper;
        private KeyBasedLogging _logger;

        public HotelShoppingService(IHttpClient client, IMapper mapper, KeyBasedLogging loging)
        {
            this.mapper = mapper;
            _model = new HotelShoppingModel(client, mapper, loging, out Information);
            _logger = loging;
        }


        public override async Task<GRPCHotelOfferReadDtoRoot> GetOfferPrice(OFFERID ID, ServerCallContext context)
        {

            var Offer = await _model.GetOfferPrice(ID.Offerid);


            try
            {
                if (Offer == null)
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
            }
            catch (RpcException ex)
            {
                QueueMessage Struct = new QueueMessage();
                Struct.AddMessage(ex.Trailers.GetValue("error-code") + " " + ex.Trailers.GetValue("error-info"));

                await MessageQueueService.SendMessage(Struct);
            }



            return mapper.Map<Hotels.GRPCHotelOfferReadDtoRoot>(Offer);
        }


        public override async Task<GRPCHotelOfferReadDtoRoot> ShopHotelOffers(HotelOffersRequestData Parameters, ServerCallContext context)
        {
            
            var Offers = await _model.ShopHotelOffers(Parameters.HotelIds, HelperMethods.EnsureDateFormat( Parameters.CheckInDate ), HelperMethods.EnsureDateFormat( Parameters.CheckOutDate ), Parameters.PriceRange, Parameters.Currency
                                , Parameters.PaymentPolicy, Parameters.BoardType, Parameters.Adults, Parameters.RoomQuantity, Parameters.IncludeClosed, Parameters.BestRateOnly );

            try
            {
                if (Offers == null)
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
            }
            catch (RpcException ex)
            {
                QueueMessage Struct = new QueueMessage();
                Struct.AddMessage(ex.Trailers.GetValue("error-code") + " " + ex.Trailers.GetValue("error-info"));
                await MessageQueueService.SendMessage(Struct);
            }



            return mapper.Map<Hotels.GRPCHotelOfferReadDtoRoot>(Offers);
        }

    }
}
