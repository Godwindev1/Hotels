using AutoMapper;
using Hotel.Data.Rating;
using Hotel.Dto.List.dto;
using Hotel.Dto.offers.dto;
using Hotel.Dto.ratings.dto;
using Hotel.model;
using Hotel.Resource.HotelList;
using Hotel.Resource.HotelOffers;

namespace Hotel.MappingProfile
{
    public class AutomapperProfile : AutoMapper.Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Hotel_d, HotelListReadDto>();
            CreateMap<HotelOffer, HotelOfferReadDto>().ForPath(x => x.room.typeEstimated, x => x.MapFrom(x => x.room.typeEstimated));
            CreateMap<HotelSentiments, HotelSentimentsReadDto>();
        }
    }
}
