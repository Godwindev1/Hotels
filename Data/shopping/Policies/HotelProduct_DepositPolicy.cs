namespace Hotel.Resource.HotelOffers.Policies
{
    public class HotelProduct_DepositPolicy
    {
        public string amount { get; set; }
        public DateTime deadline { get; set; }//recieved in json as string
        public  QualifiedFreeText description { get; set; }
        public HotelProduct_PaymentPolicy acceptedPayments { get; set; }

    }
}
