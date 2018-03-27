using System.Net;
using ConsoleChatClient.Models;

namespace ConsoleChatClient.Network {
    public interface INetworkProvider
    {
        void Connect(IPAddress serverAddress, int serverPort);
        void SendMessage(ClientMessage message);
        ServerMessage ReceiveMessageBlocking();
    }
}