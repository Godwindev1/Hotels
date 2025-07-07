using AutoMapper;
using Google.Protobuf.Collections;
using Hotel.Data.Amadeus.data.Autocomplete;
using Hotel.Data.Amadeus.data.List;
using Hotel.Data.Amadeus.data.Rating;
using Hotel.Data.Amadeus.data.shopping;
using Hotel.Dto.Amadeus.dto.autocomplete.dto;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Dto.Amadeus.dto.offers.dto;
using Hotel.Dto.Amadeus.dto.ratings.dto;
using Hotel.model;
using Hotel.Profile.Converters;
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
            CreateMap<HotelListReadDto, Hotels.GRPCHotelListReadDto>();
            CreateMap<Hotels.GRPCHotelListReadDto, HotelListReadDto>();

            CreateMap<List<HotelListReadDto>, RepeatedField<Hotels.GRPCHotelListReadDto>>().ConvertUsing<ListToRepeatedConverter>();
       
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
