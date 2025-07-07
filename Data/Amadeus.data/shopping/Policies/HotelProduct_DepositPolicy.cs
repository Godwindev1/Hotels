using Hotel.Data.Amadeus.data.shopping;

namespace Hotel.Data.Amadeus.data.shopping.Policies
{
    public class HotelProduct_DepositPolicy
    {
        public string amount { get; set; }
        public DateTime deadline { get; set; }//recieved in json as string
        public  QualifiedFreeText description { get; set; }
        public HotelProduct_PaymentPolicy acceptedPayments { get; set; }

    }
}
