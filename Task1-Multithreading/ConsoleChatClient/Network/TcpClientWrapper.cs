using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleChatClient.Models;
using Newtonsoft.Json;

namespace ConsoleChatClient.Network {
    public class TcpClientWrapper : INetworkProvider
    {
        private TcpClient _tcpClient;

        public TcpClientWrapper()
        {
            _tcpClient = new TcpClient();
        }
        public void Connect(IPAddress serverAddress, int serverPort)
        {
            _tcpClient.Connect(serverAddress, serverPort);
        }

        public ServerMessage ReceiveMessageBlocking()
        {
            byte[] buffer = new byte[1024];
            _tcpClient.GetStream().Read(buffer, 0, buffer.Length);
            var serializedMessage = Encoding.ASCII.GetString(buffer);
            return JsonConvert.DeserializeObject<ServerMessage>(serializedMessage);
        }

        public void SendMessage(ClientMessage message)
        {
            var serializedMessage = JsonConvert.SerializeObject(message);
            byte[] data = Encoding.ASCII.GetBytes(serializedMessage);
            _tcpClient.GetStream().Write(data, 0, data.Length);
        }
    }
}