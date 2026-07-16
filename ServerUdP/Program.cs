using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Серверное приложение");

const int BufferSize = 1024;

var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
socket.Bind(new IPEndPoint(IPAddress.Loopback, 11000));


while (true)
{
    var clietnIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
    var buffer = new byte[BufferSize];
    var socketReceiveFromResult = await socket.ReceiveFromAsync(buffer, clietnIpEndPoint);

    await ProcessMessageAsync(socket, buffer, socketReceiveFromResult.ReceivedBytes, socketReceiveFromResult.RemoteEndPoint);
}

static async Task ProcessMessageAsync(Socket socket, byte[] buffer, int receivedBytes, EndPoint clietnIpEndPoint)
{
    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

    Console.WriteLine($"Полученные данные: {receivedMessage}");


    var message = Encoding.UTF8.GetBytes($"Получил сообщение: {receivedMessage}");

    await socket.SendToAsync(
         message,
         SocketFlags.None,
         clietnIpEndPoint);
}