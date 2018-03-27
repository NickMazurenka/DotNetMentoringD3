using System.Net;
using System.Net.Sockets;

public class ConnectedClient
{
    public string Name { get; set; }
    public TcpClient TcpClient { get; set; }
    public string IpFormatted => ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString();
}