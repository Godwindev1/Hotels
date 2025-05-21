using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using Hotel.Services.data;

namespace Hotel.Services
{
    public class MessageQueueService : IHostedService
    {
        public MessageQueueService() 
        {

        }

        public async Task StartAsync(CancellationToken t)
        {
            await SetupFactory();
        }

        public async Task StopAsync(CancellationToken t)
        {
            await connection.CloseAsync();
        }


        public static async Task SendMessage( QueueMessage message)
        {
            if(IsInitialized)
            {
                var ParsedMessage = message.ParseMessage();

                string queueName = "GRPCServicesInformationQueue";
                string exchange = "ServicesExchange";
                string BindingKey = "Remote1@@@@@";

                await channel.ExchangeDeclareAsync("ServicesExchange", ExchangeType.Direct);

                byte[] Body;

                Body = Encoding.UTF8.GetBytes(ParsedMessage);

                Console.WriteLine("Writing Service Information for GRPCservice");
                await channel.BasicPublishAsync(exchange: exchange, routingKey: BindingKey, body: Body, mandatory: true);
            }

            else
            {
                Console.Write("Rabbitmq Connection Is not Establshed");
            }
            
        }

        public  static async Task SetupFactory()
        {
            var RabbitmqPort = int.Parse( Environment.GetEnvironmentVariable("rabbitmqport") ?? "-1");
            var HostName = Environment.GetEnvironmentVariable("rabbitmqhostname") ?? "localhost";

            try
            {
                factory = new ConnectionFactory { HostName = HostName, Port = RabbitmqPort };
                connection = await factory.CreateConnectionAsync();
                channel = await connection.CreateChannelAsync();
                IsInitialized = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                IsInitialized = false;
            }
            
        }

        static ConnectionFactory ? factory;
        static IConnection ? connection;
        static IChannel ? channel;
        public static bool IsInitialized;

    }
}
