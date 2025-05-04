using Hotel.Amadeus;
using Hotel.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Hotel.SyncServices
{
    public class HttpClientImplementation : IHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public HttpClientImplementation(HttpClient ClientInstance, IConfiguration Config)
        { 
            _httpClient = ClientInstance;
            _configuration = Config;
   
        }

        public async Task<AccessToken> RetrieveAccessToken()
        {
            var ClientID = Environment.GetEnvironmentVariable("client_id");
            var ClientSecret = Environment.GetEnvironmentVariable("client_secret");

            //var ClientID = _configuration["clientdetails:client_id"];
            //var ClientSecret = _configuration["clientdetails:client_secret"];

            Console.WriteLine($"CLientID: {ClientID} ClientSecret: {ClientSecret}");

            HttpResponseMessage Result = null;
            AccessToken Access = new AccessToken() ;
            try
            {
                HttpMessageHeaders MessageBody = new HttpMessageHeaders();
                MessageBody.Add(Constants.GRANT_TYPE, Constants.CLIENT_CREDENTIALS);
                MessageBody.Add(Constants.CLIENT_ID, ClientID);
                MessageBody.Add(Constants.CLIENT_SECRET, ClientSecret);

                Request AccessTokenRequest = new Request("POST", Constants.AUTH_URL, null, MessageBody, Constants.BASEURL);

                Console.WriteLine("Sennding Request to Amadeus API for Access Token");
                Result = await _httpClient.SendAsync(Request.ConvertToHttpRequestMessage(AccessTokenRequest));
               
                Console.WriteLine(await Result.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error In HttpClientImplementation.RetrieveAccessToken \n");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Result.EnsureSuccessStatusCode();
                var Json =  await Result.Content.ReadAsStringAsync();
                

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<AccessToken>(Json, options);
                Access.access_token = result.access_token;
            }

            return Access;
        }



        public async Task<string> SendHttpRequest(HttpRequestMessage request)
        {
            string Result = "";
            HttpResponseMessage Response = null;

            try
            {
                Response  = await _httpClient.SendAsync(request);
                Result = await Response.Content.ReadAsStringAsync();

                return Result;
            }
            catch (Exception ex)
            {
                Result = await Response.Content.ReadAsStringAsync();


                Console.WriteLine(ex.Message);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new Error() { Message = Result, StatusCode = int.Parse(Response.StatusCode.ToString()) });
            }
        }
    }
}
