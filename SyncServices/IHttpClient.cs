﻿using Hotel.Data.Amadeus.data;

namespace Hotel.SyncServices
{
    public interface IHttpClient
    {
        public  Task<AccessToken> RetrieveAccessToken();

        public  Task<string> SendHttpRequest(HttpRequestMessage request);
    }
}
