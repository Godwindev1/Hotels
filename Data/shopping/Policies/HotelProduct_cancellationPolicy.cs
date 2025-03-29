﻿namespace Hotel.Resource.HotelOffers.Policies
{
    public class HotelProduct_cancellationPolicy
    {
        public string type { get; set; }
        public string amount { get; set; }
        public int numberOfNights { get; set; }
        public string percentage { get; set; }
        public DateTime deadline {  get; set; }
        public QualifiedFreeText description {  get; set; }
    }
}
