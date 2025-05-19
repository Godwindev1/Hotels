using Hotel.Data;
using Hotel.SyncServices;
using System.Runtime.Caching;

namespace Hotel.Amadeus
{
    public class AccessTokenManager : BackgroundService
    {
        private  MemoryCache AccessTokenTimedCache = null;
        private  AccessToken accessToken;
        private  HttpClientImplementation _client;

        public AccessTokenManager(IHttpClient client)
        {
            _client = (HttpClientImplementation)client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            while (!stoppingToken.IsCancellationRequested)
            {

                Console.WriteLine("Checking for Access Token...");

                bool ConditionResults = (AccessTokenTimedCache == null) || (AccessTokenTimedCache.Get("AccessToken") == null);

                Console.WriteLine( $"Results COndition: {ConditionResults.ToString()}");

                if (AccessTokenTimedCache == null || AccessTokenTimedCache.Get("AccessToken") == null)
                {
                    Console.WriteLine("Access Token is null or expired, retrieving new token...");


                    await InitilizeRequestsWithBearerToken();

                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }

   
                await Task.Delay(1000, stoppingToken); 
            }

        }

        private async Task<AccessToken> RetrieveToken()
        {
            accessToken = await _client.RetrieveAccessToken();
            CacheAccessToken();

            return accessToken;
        }

        private  void CacheAccessToken()
        {
            AccessTokenTimedCache = MemoryCache.Default;
          
            CacheItemPolicy ExpirationPolicy = new CacheItemPolicy
            {
              AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
            };

            AccessTokenTimedCache.Add("AccessToken", accessToken.access_token, ExpirationPolicy);

        }


        private  AccessToken RetrieveTokenFromCache()
        {
           return  AccessTokenTimedCache.Get("AccessToken") as AccessToken ?? null;
        }


        public  async Task<AccessToken> GetAccessBearerToken()
        {
            if(AccessTokenTimedCache == null || AccessTokenTimedCache.Get("AccessToken") == null)
            {
               return await RetrieveToken();
            }
            else
            {
                return RetrieveTokenFromCache();
            }
        }

        public  async Task InitilizeRequestsWithBearerToken()
        {
            Request.ClearPreviousBearerToken();
            Request.InitializeBearerToken(await GetAccessBearerToken());
        }



    }
}
