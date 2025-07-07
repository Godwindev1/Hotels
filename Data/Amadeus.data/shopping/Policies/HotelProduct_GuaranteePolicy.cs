using Hotel.Data.Amadeus.data.shopping;

namespace Hotel.Data.Amadeus.data.shopping.Policies
{
    public class HotelProduct_GuaranteePolicy
    {
        public QualifiedFreeText Description { get; set; }
        public HotelProduct_PaymentPolicy acceptedPayments { get; set; }
    }
}
