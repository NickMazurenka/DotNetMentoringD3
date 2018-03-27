using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ConsoleChatClient.Models;
using ConsoleChatClient.Network;
using Newtonsoft.Json;

public class Client
{
    private ConsoleChatOutput _chatOutput;
    private IPAddress _serverAddress;
    private int _serverPort;
    private INetworkProvider _networkProvider;

    public Client(INetworkProvider networkProvider, IPAddress serverAddress, int serverPort, ConsoleChatOutput chatOutput)
    {
        _serverAddress = serverAddress;
        _serverPort = serverPort;
        _networkProvider = networkProvider;
        _chatOutput = chatOutput;
    }

    public void Start()
    {
        _networkProvider.Connect(_serverAddress, _serverPort);
        Task.Factory.StartNew(ProcessServerMessages);
        while (true)
        {
            var text = Console.ReadLine();
            _networkProvider.SendMessage(new ClientMessage(text));
        }
    }

    private void ProcessServerMessages()
    {
        while (true)
        {
            var message = _networkProvider.ReceiveMessageBlocking();
            _chatOutput.WriteMessage(message);
        }
    }
}