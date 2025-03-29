namespace Hotel.Resource.HotelOffers.Policies
{
    public class HotelProduct_checkInOutPolicy
    {
        public string checkIn {  get; set; }
        public QualifiedFreeText checkInDescription { get; set; }
        public string checkOut { get; set; }
        public QualifiedFreeText checkOutDescription { get; set; }
    }
}
