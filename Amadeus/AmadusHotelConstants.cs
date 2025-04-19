namespace Hotel.Amadeus
{
    public class AmadeusHotelConstants
    {
        /// <summary>
        /// The city code.
        /// </summary>
        public const string CityCode = "cityCode";
        
        public const string offerId = "offerId";

        /// <summary>
        /// The search radius.
        /// </summary>
        public const string Radius = "radius";

        /// <summary>
        /// The unit of the radius (e.g., km, miles).
        /// </summary>
        public const string RadiusUnit = "radiusUnit";

        /// <summary>
        /// The chain codes.
        /// </summary>
        public const string ChainCodes = "chainCodes";

        /// <summary>
        /// The amenities to search for.
        /// </summary>
        public const string Amenities = "amenities";

        /// <summary>
        /// The hotel ratings.
        /// </summary>
        public const string Ratings = "ratings";

        /// <summary>
        /// The optional hotel source.
        /// </summary>
        public const string HotelSource = "hotelSource"; // Optional field

        /// <summary>
        /// The geo code latitude.
        /// </summary>
        public const string Latitude = "latitude";

        /// <summary>
        /// The geo code longitude.
        /// </summary>
        public const string Longitude = "longitude";

        /// <summary>
        /// The hotel ID.
        /// </summary>
        public const string HotelId = "hotelIds";

        // Newly added constants
        /// <summary>
        /// Number of adults.
        /// </summary>
        public const string Adults = "adults";

        /// <summary>
        /// Check-in date.
        /// </summary>
        public const string CheckInDate = "checkInDate";
       
        public const string checkOutDate = "checkOutDate";
        
        public const string countryOfResidence = "countryOfResidence";

        public const string priceRange = "priceRange";
      
        public const string currency = "currency";
     
        public const string paymentPolicy = "paymentPolicy";
      
        public const string boardType = "boardType";
       
        public const string includeClosed = "includeClosed";
      
        public const string bestRateOnly = "bestRateOnly";
      
        public const string keyword = "keyword";
        public const string subType = "subType";
        public const string countryCode = "countryCode";
        public const string lang = "lang";
        public const string max = "max";

        /// <summary>
        /// Room quantity.
        /// </summary>
        public const string RoomQuantity = "roomQuantity";

        public const string HotelSentiments = "v2/e-reputation/hotel-sentiments";
      
        public const string HotelByID = "v1/reference-data/locations/hotels/by-hotels";
        public const string HotelByCityURL = "v1/reference-data/locations/hotels/by-city";
        public const string HotelByGeoCode = "v1/reference-data/locations/hotels/by-geocode";
       
        public const string ShoppingOffersHotelID = "v3/shopping/hotel-offers";
        public const string AutoComplete = "v1/reference-data/locations/hotel";
    }





    public class AmenitiesConstants
    {
        /// <summary>
        /// Amenity for fitness center.
        /// </summary>
        public const string FitnessCenter = "FITNESS_CENTER";

        /// <summary>
        /// Amenity for air conditioning.
        /// </summary>
        public const string AirConditioning = "AIR_CONDITIONING";

        /// <summary>
        /// Amenity for restaurant.
        /// </summary>
        public const string Restaurant = "RESTAURANT";

        /// <summary>
        /// Amenity for parking.
        /// </summary>
        public const string Parking = "PARKING";

        /// <summary>
        /// Amenity for pets allowed.
        /// </summary>
        public const string PetsAllowed = "PETS_ALLOWED";

        /// <summary>
        /// Amenity for airport shuttle.
        /// </summary>
        public const string AirportShuttle = "AIRPORT_SHUTTLE";

        /// <summary>
        /// Amenity for business center.
        /// </summary>
        public const string BusinessCenter = "BUSINESS_CENTER";

        /// <summary>
        /// Amenity for disabled facilities.
        /// </summary>
        public const string DisabledFacilities = "DISABLED_FACILITIES";

        /// <summary>
        /// Amenity for Wi-Fi.
        /// </summary>
        public const string WiFi = "WIFI";

        /// <summary>
        /// Amenity for meeting rooms.
        /// </summary>
        public const string MeetingRooms = "MEETING_ROOMS";

        /// <summary>
        /// Amenity for no kids allowed.
        /// </summary>
        public const string NoKidAllowed = "NO_KID_ALLOWED";

        /// <summary>
        /// Amenity for tennis.
        /// </summary>
        public const string Tennis = "TENNIS";

        /// <summary>
        /// Amenity for golf.
        /// </summary>
        public const string Golf = "GOLF";

        /// <summary>
        /// Amenity for kitchen.
        /// </summary>
        public const string Kitchen = "KITCHEN";

        /// <summary>
        /// Amenity for animal watching.
        /// </summary>
        public const string AnimalWatching = "ANIMAL_WATCHING";

        /// <summary>
        /// Amenity for baby-sitting.
        /// </summary>
        public const string BabySitting = "BABY-SITTING";

        /// <summary>
        /// Amenity for beach.
        /// </summary>
        public const string Beach = "BEACH";

        /// <summary>
        /// Amenity for casino.
        /// </summary>
        public const string Casino = "CASINO";

        /// <summary>
        /// Amenity for jacuzzi.
        /// </summary>
        public const string Jacuzzi = "JACUZZI";

        /// <summary>
        /// Amenity for sauna.
        /// </summary>
        public const string Sauna = "SAUNA";

        /// <summary>
        /// Amenity for solarium.
        /// </summary>
        public const string Solarium = "SOLARIUM";

        /// <summary>
        /// Amenity for massage.
        /// </summary>
        public const string Massage = "MASSAGE";

        /// <summary>
        /// Amenity for valet parking.
        /// </summary>
        public const string ValetParking = "VALET_PARKING";

        /// <summary>
        /// Amenity for bar or lounge.
        /// </summary>
        public const string BarOrLounge = "BAR or LOUNGE";

        /// <summary>
        /// Amenity for kids welcome.
        /// </summary>
        public const string KidsWelcome = "KIDS_WELCOME";

        /// <summary>
        /// Amenity for no pornographic films.
        /// </summary>
        public const string NoPornFilms = "NO_PORN_FILMS";

        /// <summary>
        /// Amenity for minibar.
        /// </summary>
        public const string MiniBar = "MINIBAR";

        /// <summary>
        /// Amenity for television.
        /// </summary>
        public const string Television = "TELEVISION";

        /// <summary>
        /// Amenity for Wi-Fi in room.
        /// </summary>
        public const string WiFiInRoom = "WI-FI_IN_ROOM";

        /// <summary>
        /// Amenity for room service.
        /// </summary>
        public const string RoomService = "ROOM_SERVICE";

        /// <summary>
        /// Amenity for guarded parking.
        /// </summary>
        public const string GuardedParking = "GUARDED_PARKG";

        /// <summary>
        /// Amenity for special service menu.
        /// </summary>
        public const string SpecialServiceMenu = "SERV_SPEC_MENU";
    }
}
