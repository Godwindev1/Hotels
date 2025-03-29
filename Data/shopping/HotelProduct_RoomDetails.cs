namespace Hotel.Resource.HotelOffers
{
    public class HotelProduct_RoomDetails
    {
        public string type { get; set; }
        public HotelProduct_EstimatedRoomType typeEstimated {  get; set; }
        public QualifiedFreeText description { get; set; }
    }
}
