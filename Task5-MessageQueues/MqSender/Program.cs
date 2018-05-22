using System;
using System.IO;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace MqClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var queueClient = new QueueClient(configuration.GetConnectionString("ServiceBus"), configuration["QueueName"]);

            var message = new Message(Encoding.UTF8.GetBytes("Hello"));

            queueClient.SendAsync(message).Wait();

            Console.ReadKey();

            queueClient.CloseAsync().Wait();
        }
    }
}
