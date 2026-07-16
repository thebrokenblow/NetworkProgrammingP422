using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Клиентское приложение");

var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
var serverEndPoint = new IPEndPoint(IPAddress.Loopback, 11000);

var clietnIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

while (true)
{
    var receiveBuffer = new byte[65507];

    Console.WriteLine("Введите данные: ");
    var message = Console.ReadLine();
    var sendBuffer = Encoding.UTF8.GetBytes(message);

    await socket.SendToAsync(
          sendBuffer,
          SocketFlags.None,
          serverEndPoint);

    var result = await socket.ReceiveFromAsync(
          receiveBuffer,
          SocketFlags.None,
          clietnIpEndPoint);

    string response = Encoding.UTF8.GetString(receiveBuffer, 0, result.ReceivedBytes);
    Console.WriteLine($"Ответ от сервера: {response}");
}