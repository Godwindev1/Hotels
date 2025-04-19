
using AutoMapper;
using Hotel.Amadeus;
using Hotel.Logging;
using Hotel.MappingProfile;
using Hotel.model;
using Hotel.Services;
using Hotel.SyncServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace Hotel
{
    public class Program
    {
        public  static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient<IHttpClient, HttpClientImplementation>();
            builder.Services.AddScoped<AutoMapper.Profile, AutomapperProfile>();
            builder.Services.AddSingleton(new KeyBasedLogging());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddGrpc();  // Add gRPC


            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2; // HTTP/2 only
                    listenOptions.UseHttps(); // gRPC requires HTTPS
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var HttpClient = scope.ServiceProvider.GetRequiredService<IHttpClient>();
                AccessTokenManager Test = new AccessTokenManager(HttpClient);
                Test.InitilizeRequestsWithBearerToken();
            }

            app.MapGrpcService<HotelListService>();  // Register Hotel Grpc service
            app.MapGrpcService<HotelShoppingService>();  // Register Hotel Shopping Grpc service
            app.MapGrpcService<AutocompleteService>();  // Register Autocomplete Grpc service
            app.MapGrpcService<HotelSentimentService>();  // Register Sentiments Grpc service

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
