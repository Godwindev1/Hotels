﻿using Hotel.Resource.HotelList;

namespace Hotel.Dto.autocomplete.dto
{
    public class AutoCompleteReadDto
    {
        public string name { get; set; }
        public string iataCode { get; set; }

        public string subType { get; set; }

        public int relevance { get; set; } 
        public List<string> hotelIds { get; set; }
        public Address Address { get; set; }
    }
}
