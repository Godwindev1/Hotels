using Hotel.Resource.HotelList;

namespace Hotel.Dto.List.dto
{
    public class HotelListReadDto
    {
        public string hotelId { get; set; }
        public string name { get; set; }
        public List<string>? amenities { get; set; }
        public int rating { get; set; }

        public Distance distance { get; set; }


    }
}
