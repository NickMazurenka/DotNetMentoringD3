using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Client
{
    private ConsoleChatOutput _chatOutput;
    private IPAddress _serverAddress;
    private int _serverPort;
    private TcpClient _tcpClient;

    public Client(TcpClient tcpClient, IPAddress serverAddress, int serverPort, ConsoleChatOutput chatOutput)
    {
        _serverAddress = serverAddress;
        _serverPort = serverPort;
        _tcpClient = tcpClient;
        _chatOutput = chatOutput;
    }

    public void Start()
    {
        _tcpClient.Connect(_serverAddress, _serverPort);
        Task.Factory.StartNew(ProcessServerMessages);
        while (true)
        {
            var text = Console.ReadLine();
            SendMessage(new ClientMessage(text));
        }
    }

    private void SendMessage(ClientMessage message)
    {
        var serializedMessage = JsonConvert.SerializeObject(message);
        byte[] data = Encoding.ASCII.GetBytes(serializedMessage);
        _tcpClient.GetStream().Write(data, 0, data.Length);
    }

    private ServerMessage ReceiveMessage()
    {
        byte[] buffer = new byte[1024];
        _tcpClient.GetStream().Read(buffer, 0, buffer.Length);
        var serializedMessage = Encoding.ASCII.GetString(buffer);
        return JsonConvert.DeserializeObject<ServerMessage>(serializedMessage);
    }

    private void ProcessServerMessages()
    {
        while (true)
        {
            var message = ReceiveMessage();
            _chatOutput.WriteMessage(message);
        }
    }
}