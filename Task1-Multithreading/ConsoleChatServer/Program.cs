using System;
using System.Net;

namespace ConsoleChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 7227;
            var server = new Server(address, port);
            server.Start();
            Console.ReadLine();
        }
    }
}
