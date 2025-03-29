namespace Hotel.Resource.HotelOffers
{
    public struct Tax
    {
        /// <summary>
        /// An impost for raising revenue for the general treasury and which will be used for general public purposes.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Defines amount with decimal separator.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Defines a monetary unit. It is a three alpha code (IATA code). Example: EUR for Euros, USD for US dollar, etc.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// International Standards Organization (ISO) Tax code. It is a two-letter country code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// In the case of a tax on TST value, the percentage of the tax will be indicated in this field.
        /// </summary>
        public string Percentage { get; set; }

        /// <summary>
        /// Indicates if tax is included or not.
        /// </summary>
        public bool Included { get; set; }

        /// <summary>
        /// Specifies if the tax applies per stay or per night.
        /// </summary>
        public string PricingFrequency { get; set; } // Use enum if possible (PER_STAY, PER_NIGHT)

        /// <summary>
        /// Specifies if the tax applies per occupant or per room.
        /// </summary>
        public string PricingMode { get; set; } // Use enum if possible (PER_OCCUPANT, PER_PRODUCT)
    }

}
