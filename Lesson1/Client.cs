using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lesson1;

public class Client
{
    private readonly Socket _tcpSocket;
    private readonly IPEndPoint _iPEndPoint;

    private const int sizeBuffer = 1024;

    public Client(string ipAddress, int port)
    {
        if (IPAddress.TryParse(ipAddress, out IPAddress? validationIpAddress))
        {
            _iPEndPoint = new IPEndPoint(validationIpAddress, port);
        }
        else
        {
            var Ips = Dns.GetHostAddresses(ipAddress);

            if (Ips.Length == 0)
            {
                throw new Exception();
            }
            else
            {
                _iPEndPoint = new IPEndPoint(Ips.First(), port);
            }
        }

        _tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }


    public Client(IPEndPoint iPEndPoint)
    {
        _iPEndPoint = iPEndPoint;
        _tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public async Task ConnectionAsync()
    {
        await _tcpSocket.ConnectAsync(_iPEndPoint);
    }

    public async Task SendAsync(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await _tcpSocket.SendAsync(messageBytes);
    }

    public async Task<string> ReadAsync()
    {
        int bytes;
        var responseBytes = new byte[sizeBuffer];
        var stringBuilder = new StringBuilder();

        do
        {
            bytes = await _tcpSocket.ReceiveAsync(responseBytes);
            string responsePart = Encoding.UTF8.GetString(responseBytes, 0, bytes);
            stringBuilder.Append(responsePart);
        }
        while (bytes > 0);

        return stringBuilder.ToString();
    }
}
