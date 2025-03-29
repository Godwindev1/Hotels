

using Hotel.Resource.HotelList;

namespace Hotel.Resource.HotelOffers
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
