using System.Net;
using System.Net.Sockets;

public class Client
{
    public string Name { get; set; }
    public TcpClient TcpClient { get; set; }
    public string IpFormatted => ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString();
}