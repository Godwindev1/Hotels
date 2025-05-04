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
            bool tokenRetrievedDelay = false;
           
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking for Access Token...");

                if (AccessTokenTimedCache == null || AccessTokenTimedCache.Get("AccessToken") == null)
                {
                    Console.WriteLine("Access Token is null or expired, retrieving new token...");

                    await InitilizeRequestsWithBearerToken();

                    tokenRetrievedDelay = true;
                    await Task.Delay(TimeSpan.FromMinutes(28), stoppingToken);
                    tokenRetrievedDelay = false ;
                }

                if (!tokenRetrievedDelay)
                {
                    await Task.Delay(5000, stoppingToken); 
                }
            }

        }

        private    async Task<AccessToken> RetrieveToken()
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
            if(AccessTokenTimedCache == null)
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
            Request.InitializeBearerToken(await GetAccessBearerToken());
        }



    }
}
