namespace Hotel.Resource.HotelOffers.Policies
{
    public class HotelProduct_GuaranteePolicy
    {
        public QualifiedFreeText Description { get; set; }
        public HotelProduct_PaymentPolicy acceptedPayments { get; set; }
    }
}
