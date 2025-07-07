namespace Hotel.Data.Amadeus.data.shopping
{
    public class HotelProduct_Price
    {
        public string currency { get; set; }
        public string sellingTotal { get; set; }
        public string total { get; set; }
        public string Base {get; set;}

        public List<Tax> taxes { get; set; }
        public List<Markup> markups { get; set; }
        public HotelProduct_PriceVariations variations { get; set; }
    }
}
