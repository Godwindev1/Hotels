using Hotel.Resource.HotelOffers.Policies;

namespace Hotel.Resource.HotelOffers
{
    public class HotelProduct_policyDetails
    {
        //payment type. Guarantee means Pay at Check Out. Check the methods in guarantee or deposit or prepay
        public string paymentType { get; set; }
        public HotelProduct_GuaranteePolicy guarantee {  get; set; }

        public HotelProduct_DepositPolicy deposit { get; set; }
        public HotelProduct_DepositPolicy prepay { get; set; }

        public HotelProduct_HoldPolicy holdTime { get; set; }

        public List<HotelProduct_cancellationPolicy> cancellations { get; set; }

        public HotelProduct_checkInOutPolicy checkInOut { get; set; }
    }
}
