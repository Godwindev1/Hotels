namespace Hotel.Resource.HotelOffers
{

    public struct Price
    {
        /// <summary>
        /// Price valuation information.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Currency code applied to all elements of the price.
        /// Example: EUR for Euros, USD for US dollar, etc.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Selling total, calculated as:
        /// Total + Margins + Markup + TotalFees - Discounts.
        /// </summary>
        public string SellingTotal { get; set; }

        /// <summary>
        /// Total price, calculated as:
        /// Base + TotalTaxes.
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// Base price before any taxes, fees, or markups are applied.
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// A list of markups applied to the base price.
        /// </summary>
        public List<string> Markups { get; set; } // Use a more specific type if markups have a detailed structure
    }



    public struct HotelProductPriceVariation
    {
        /// <summary>
        /// Some prices may vary during a stay, thus here you can see the daily price per period of the stay.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Begin date of the period.
        /// Format: YYYY-MM-DD.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the period.
        /// Format: YYYY-MM-DD.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Currency code applied to all elements of the price.
        /// Example: EUR for Euros, USD for US dollars, etc.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Selling total, calculated as:
        /// Total + Margins + Markup + TotalFees - Discounts.
        /// </summary>
        public string SellingTotal { get; set; }

        /// <summary>
        /// Total price, calculated as:
        /// Base + TotalTaxes.
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// Base price before any taxes, fees, or markups are applied.
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// A list of markups applied to the base price.
        /// </summary>
        public List<string> Markups { get; set; } // Replace with a specific type if needed for markups
    }


    public class HotelProduct_PriceVariations
    {
        public Price average {  get; set; }
        public List<HotelProductPriceVariation> changes { get; set; }
    }
}
