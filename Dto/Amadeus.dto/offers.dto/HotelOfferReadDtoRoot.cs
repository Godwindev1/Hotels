using Hotel.Dto.Amadeus.dto.List.dto;

namespace Hotel.Dto.Amadeus.dto.offers.dto
{
    public class HotelOfferReadDtoRoot
    {
       public List<HotelOfferReadDto> data { get; set; }

       public bool Available { get; set; } //this value is manually set in hotelShoppingModel methods

       public HotelListReadDto hotel { get; set; }
    }
}
