using static Hotel.Data.Rating.HotelSentiments;
namespace Hotel.Dto.ratings.dto
{
    public class HotelSentimentsReadDto
    {
        public Sentiment sentiments { get; set; }
        public string hotelId { get; set; }
        public int overallRating { get; set; }

        public int numberOfRatings { get; set; }

    }
}
