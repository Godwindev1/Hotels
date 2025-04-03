using Hotel.Resource.HotelList;

namespace Hotel.Data.Autocomplete
{
    public class AutoCompleteInfo
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string iataCode { get; set; }

        public string subType { get; set; }

        public int relevance { get; set; }

        public string type { get; set; }

        public List<string> hotelIds { get; set; }
        public Address Address { get; set; }
        public GeoCode geoCode { get; set; }
    }
}
