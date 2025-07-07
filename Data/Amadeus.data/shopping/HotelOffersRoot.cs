using Hotel.Data.Amadeus.data.List;

namespace Hotel.Data.Amadeus.data.shopping
{
    public class HotelOffersData
    {
        public string type { get; set; }
        public List<HotelOffer> offers { get; set; }
        public bool available { get; set; }

        public Hotel_d hotel { get; set; }
    }

    public class HotelOfferRoot
    {
        public HotelOffersData data { get; set; }
    }

}
