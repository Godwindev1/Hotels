namespace Hotel.Data.Amadeus.data.Rating
{
  public class HotelSentiments
  {
     public string hotelId { get; set; }

     public string type { get; set; }
     public int overallRating { get; set; }
   

     public int numberOfRatings { get; set; }
     public int numberOfReviews { get; set; }
     public class  Sentiment
     {
        public int sleepQuality { get; set; }

        public int service { get; set; }

        public int facilities { get; set; }

        public int roomComforts { get; set; }

        public int valueForMoney { get; set; }

        public int catering { get; set; }

        public int swimmingPool { get; set; }

        public int location { get; set; }

        public int internet { get; set; }

        public int pointsOfInterest { get; set; }

        public int staff { get; set; }

     }

        public Sentiment sentiments {get; set;}

  }

}
