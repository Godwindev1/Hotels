using static Hotel.Data.Amadeus.data.Rating.HotelSentiments;
namespace Hotel.Dto.Amadeus.dto.ratings.dto
{
    public class HotelSentimentsReadDto
    {
        public Sentiment sentiments { get; set; }
        public string hotelId { get; set; }
        public int overallRating { get; set; }

        public int numberOfRatings { get; set; }

    }
}
