using Hotel.Dto.List.dto;
using Hotel.Resource.HotelOffers;

namespace Hotel.Dto.offers.dto
{
    public class HotelOfferReadDto
    {
        public string id { get; set; }

        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }

        public string roomQuantity { get; set; }

        public List<string> boardType { get; set; }

        public RoomDetailsReadDto room { get; set; }

        public HotelProduct_Guests guests { get; set; }

        public HotelProduct_Price price { get; set; }

 
    }
}
