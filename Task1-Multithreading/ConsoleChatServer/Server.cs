using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Server
{
    private TcpListener _listener;
    private List<ConnectedClient> _clients = new List<ConnectedClient>();

    public Server(IPAddress address, int port)
    {
        _listener = new TcpListener(address, port);
    }

    public void Start()
    {
        _listener.Start();

        while (true)
        {
            Console.WriteLine("Waiting for client...");
            var clientTask = _listener.AcceptTcpClientAsync();

            if (clientTask.Result != null)
            {
                var client = new ConnectedClient
                {
                    Name = null,
                    TcpClient = clientTask.Result
                };
                _clients.Add(client);
                Log(String.Format("Client with IP {0} connected", client.IpFormatted), ConsoleColor.Green);

                Task.Factory
                    .StartNew(() => AssignName(client))
                    .ContinueWith(_ => ProcessClientMessages(client), TaskContinuationOptions.NotOnFaulted)
                    .ContinueWith(_ => RemoveClient(client), TaskContinuationOptions.OnlyOnFaulted);
            }
        }
    }

    private void AssignName(ConnectedClient client)
    {
        SendMessage(client, new ServerMessage("Hello, Anonymous. Choose your name. Length between 2 and 16 symbols"));
        while (true)
        {
            var nameMessage = ReceiveMessage(client.TcpClient);
            if (nameMessage.Text.Length < 2 || nameMessage.Text.Length > 16)
            {
                SendMessage(client, new ServerMessage("Invalid name. Length should be between 2 and 16 symbols"));
            }
            else if (nameMessage.Text.ToLowerInvariant() == "server")
            {
                SendMessage(client, new ServerMessage("Nice try. This name is reserved"));
            }
            else if (_clients.Any(c => c.Name == nameMessage.Text.ToLowerInvariant()))
            {
                SendMessage(client, new ServerMessage("This name is already taken. Try another name"));
            }
            else
            {
                client.Name = nameMessage.Text;
                SendMessage(client, new ServerMessage("Welcome to chat, " + client.Name));
                break;
            }
        }
    }

    private void ProcessClientMessages(ConnectedClient clientToProcess)
    {
        while (true)
        {
            ClientMessage receivedMessage;
            try
            {
                receivedMessage = ReceiveMessage(clientToProcess.TcpClient);
                var message = new ServerMessage(receivedMessage.Text, clientToProcess.Name, receivedMessage.Created);
                Console.Write("Client to process: \"" + clientToProcess.Name + "\". Receivers: ");
                foreach (var client in _clients)
                {
                    Console.Write("\"" + client.Name + "\" ");
                    if (client == clientToProcess)
                    {
                        message.ClientMessage = true;
                    }
                    else
                    {
                        message.ClientMessage = false;
                    }
                    SendMessage(client, message);
                }
                Console.WriteLine();
            }
            catch (IOException)
            {
                RemoveClient(clientToProcess);
                break;
            }
        }
    }

    private void BroadcastServerMessage(ServerMessage message)
    {
        foreach (var client in _clients)
        {
            SendMessage(client, message);
        }
    }

    private ClientMessage ReceiveMessage(TcpClient tcpClient)
    {
        byte[] buffer = new byte[1024];
        tcpClient.GetStream().Read(buffer, 0, buffer.Length);
        var serializedMessage = Encoding.ASCII.GetString(buffer);
        return JsonConvert.DeserializeObject<ClientMessage>(serializedMessage);
    }

    private void SendMessage(ConnectedClient client, ServerMessage message)
    {
        var serializedMessage = JsonConvert.SerializeObject(message);
        byte[] data = Encoding.ASCII.GetBytes(serializedMessage);
        client.TcpClient.GetStream().Write(data, 0, data.Length);
    }

    private void RemoveClient(ConnectedClient client)
    {
        var clientName = client.Name != null ? client.Name : "Anonymous";
        Log(String.Format("Client {0} with IP {1} disconnected", clientName, client.IpFormatted), ConsoleColor.Red);
        if (client.TcpClient.Connected)
        {
            client.TcpClient.GetStream().Dispose();
        }
        _clients.Remove(client);
        if (clientName != "Anonymous")
        {
            BroadcastServerMessage(new ServerMessage("Client " + clientName + " disconnected"));
        }
    }

    private void Log(string text, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(DateTime.Now.ToString("yy/MM/dd HHmmss "));
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = previousColor;
    }
}