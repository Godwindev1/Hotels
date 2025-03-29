using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotel.Resource.HotelList
{

    //_d is to Differentiate this From The Namespace
    public class Hotel_d
    {
        public string chainCode { get; set; }

        //contains City CODE used by amadeus eg PAR for paris
        public string iataCode { get; set; }
        public int dupeId { get; set; }

        public string name { get; set; }

        public string hotelId { get; set; }

        public Distance distance { get; set; }
        public GeoCode geoCode { get; set; }
        public Address Address { get; set; }

        public List<string>? amenities { get; set; }

        public DateTime lastUpdate { get; set; }
    }


}


