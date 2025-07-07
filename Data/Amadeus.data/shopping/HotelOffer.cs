namespace Hotel.Data.Amadeus.data.shopping
{
    public class HotelOffer
    {
        public string id { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public string roomQuantity { get; set; }
        public string rateCode { get; set; }

        public HotelProduct_rateFamily rateFamilyEstimated { get; set; }

        public string category { get; set; }

        public QualifiedFreeText description { get; set; }
        public HotelProduct_Comission commission { get; set; }
        public List<string> boardType { get; set; }

        public HotelProduct_RoomDetails room {  get; set; }
        public HotelProduct_Guests guests { get; set; }
        public HotelProduct_Price price { get; set; }

        public HotelProduct_policyDetails policies { get; set; }
        public string self { get; set; }
    }
}
