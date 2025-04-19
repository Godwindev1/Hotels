using AutoMapper;
using Google.Protobuf.Collections;
using Hotel.Data.Autocomplete;
using Hotel.Data.Rating;
using Hotel.Dto.autocomplete.dto;
using Hotel.Dto.List.dto;
using Hotel.Dto.offers.dto;
using Hotel.Dto.ratings.dto;
using Hotel.model;
using Hotel.Profile.Converters;
using Hotel.Resource.HotelList;
using Hotel.Resource.HotelOffers;
using Hotels;
using static Hotels.GRPCListOfHotelSentiments.Types;

namespace Hotel.MappingProfile
{
    public class AutomapperProfile : AutoMapper.Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Hotel_d, HotelListReadDto>();
            CreateMap<HotelOffer, HotelOfferReadDto>().ForPath(x => x.room.typeEstimated, x => x.MapFrom(x => x.room.typeEstimated));
            CreateMap<HotelSentiments, HotelSentimentsReadDto>();
            CreateMap<AutoCompleteInfo, AutoCompleteReadDto>();
            CreateMap<Hotel.Dto.List.dto.HotelListReadDto, Hotels.GRPCHotelListReadDto>();
            CreateMap<Hotels.GRPCHotelListReadDto, Hotel.Dto.List.dto.HotelListReadDto>();

            CreateMap<List<Hotel.Dto.List.dto.HotelListReadDto>, RepeatedField<Hotels.GRPCHotelListReadDto>>().ConvertUsing<ListToRepeatedConverter>();
       
            CreateMap<HotelOfferReadDto, GRPCHotelOfferReadDto>().
                ForMember(x => x.CheckInDate, z => z.MapFrom( y => y.checkInDate.ToString()))
                .ForMember(x => x.CheckOutDate, z => z.MapFrom(y => y.checkOutDate.ToString()));
           
            CreateMap<HotelOfferReadDtoRoot, GRPCHotelOfferReadDtoRoot>();

            CreateMap<Address, GRPCAddress>();
            CreateMap<AutoCompleteReadDto, GRPCAutoCompleteReadDto>();

            CreateMap<HotelSentiments.Sentiment, GRPCSentiments>();
            CreateMap<HotelSentimentsReadDto, GRPCHotelSentimentsReadDto>();

            //TODO: try it without Conveters
            //CreateMap<List<HotelSentimentsReadDto>, RepeatedField<GRPCHotelSentimentsReadDto>>().ConvertUsing<ListToRepeatedHotelSentiments>();
        }
    }
}
