
using AutoMapper;
using Hotel.Amadeus;
using Hotel.Logging;
using Hotel.MappingProfile;
using Hotel.model;
using Hotel.SyncServices;

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
            builder.Services.AddScoped<Profile, AutomapperProfile>();
            builder.Services.AddSingleton(new KeyBasedLogging());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var HttpClient = scope.ServiceProvider.GetRequiredService<IHttpClient>();
                AccessTokenManager Test = new AccessTokenManager(HttpClient);
                Test.InitilizeRequestsWithBearerToken();
            }



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
