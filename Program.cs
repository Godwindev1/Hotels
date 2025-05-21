
using AutoMapper;
using Hotel.Amadeus;
using Hotel.Logging;
using Hotel.MappingProfile;
using Hotel.model;
using Hotel.Services;
using Hotel.SyncServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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


             builder.Services.AddHttpClient<IHttpClient, HttpClientImplementation>().ConfigurePrimaryHttpMessageHandler(() =>
                  {
                      var handler = new HttpClientHandler();

                      var caCert = new X509Certificate2("/etc/ssl/certs/ca.crt");

                      handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                      {
                          // Add the CA certificate to the chain
                          chain.ChainPolicy.ExtraStore.Add(caCert);

                          // Allow unknown CAs (since it's self-signed)
                          chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;
                          chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

                          // Build the chain and verify manually
                          bool isValid = chain.Build(cert);

                          return isValid;
                      };

                      return handler;
                  });   

            builder.Services.AddHttpClient<IHttpClient, HttpClientImplementation>();



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<AutoMapper.Profile, AutomapperProfile>();
            builder.Services.AddSingleton(new KeyBasedLogging());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddGrpc();  // Add gRPC
            builder.Services.AddHostedService<AccessTokenManager>();
            builder.Services.AddHostedService<MessageQueueService>();



             builder.WebHost.ConfigureKestrel(options =>
            {
               options.ConfigureHttpsDefaults(httpsOptions =>
                {                     
                    var certPath = "/etc/ssl/certs/tls.crt";
                    var keyPath =  "/etc/ssl/certs/tls.key";
                    
                    Console.WriteLine($"Certificate Setup CertPath: {certPath} KeyPath: {keyPath}");
                    
                    httpsOptions.ServerCertificate = X509Certificate2.CreateFromPemFile(certPath, keyPath);
                    httpsOptions.ServerCertificate = X509Certificate2.CreateFromPemFile(certPath, keyPath);

                });


                options.Listen(IPAddress.Any, 5001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2; // both HTTP/1.1 and HTTP/2
                    listenOptions.UseHttps(); // gRPC requires HTTPS
                });
            });  

            var app = builder.Build();


            app.MapGrpcService<HotelListService>();  // Register Hotel Grpc service
            app.MapGrpcService<HotelShoppingService>();  // Register Hotel Shopping Grpc service
            app.MapGrpcService<AutocompleteService>();  // Register Autocomplete Grpc service
            app.MapGrpcService<HotelSentimentService>();  // Register Sentiments Grpc service

            
            // Configure the HTTP request pipeline.
           // if (app.Environment.IsDevelopment())
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
