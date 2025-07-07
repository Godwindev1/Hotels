using AutoMapper;
using Google.Protobuf.Collections;
using Hotel.Dto.Amadeus.dto.List.dto;
using Hotel.Dto.Amadeus.dto.ratings.dto;
using static Hotels.GRPCListOfHotelSentiments.Types;

namespace Hotel.Profile.Converters
{

    public class ListToRepeatedConverter : ITypeConverter<List<HotelListReadDto>, RepeatedField<Hotels.GRPCHotelListReadDto>>
    {
        public RepeatedField<Hotels.GRPCHotelListReadDto> Convert(List<HotelListReadDto> source, RepeatedField<Hotels.GRPCHotelListReadDto> destination, ResolutionContext context)
        {
            var repeated = new RepeatedField<Hotels.GRPCHotelListReadDto>();

            repeated.AddRange(context.Mapper.Map<List<Hotels.GRPCHotelListReadDto>>(source));

            return repeated;
        }
    }


    public class ListToRepeatedHotelSentiments : ITypeConverter<List<HotelSentimentsReadDto>, RepeatedField<GRPCHotelSentimentsReadDto>>
    {
        public RepeatedField<GRPCHotelSentimentsReadDto> Convert(List<HotelSentimentsReadDto> source, RepeatedField<GRPCHotelSentimentsReadDto> destination, ResolutionContext context)
        {
            var repeated = new RepeatedField<GRPCHotelSentimentsReadDto>();

            repeated.AddRange(context.Mapper.Map<List<GRPCHotelSentimentsReadDto>>(source));

            return repeated;
        }
    }

}
