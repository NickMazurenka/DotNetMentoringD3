﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ConsoleChat
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 7227;
            Console.WriteLine("Hello from console chat!");
            var client = new Client(new TcpClient(), address, port, new ConsoleChatOutput());
            client.Start();
        }
    }
}
