using System.Net;
using System.Net.Sockets;
using System.Text;

const int ServerPort = 11000;
const string ServerAddress = "127.0.0.1";

using var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

var serverEndPoint = new IPEndPoint(IPAddress.Parse(ServerAddress), ServerPort);

Console.WriteLine("UDP-клиент (асинхронный) запущен. Введите сообщение для отправки (exit - выход):");

byte[] receiveBuffer = new byte[1024];
EndPoint serverReplyEndPoint = new IPEndPoint(IPAddress.Any, 0);

while (true)
{
    Console.Write("> ");
    string message = Console.ReadLine();

    if (string.Equals(message, "exit", StringComparison.OrdinalIgnoreCase))
        break;

    try
    {
        // Отправляем сообщение серверу (асинхронно)
        byte[] sendBuffer = Encoding.UTF8.GetBytes(message);
        await clientSocket.SendToAsync(
            sendBuffer,
            SocketFlags.None,
            serverEndPoint);

        // Принимаем ответ (асинхронно)
        SocketReceiveFromResult result = await clientSocket.ReceiveFromAsync(
            receiveBuffer,
            SocketFlags.None,
            serverReplyEndPoint);

        int bytesReceived = result.ReceivedBytes;
        EndPoint remoteEndPoint = result.RemoteEndPoint; // адрес сервера (можно не использовать)

        string response = Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived);
        Console.WriteLine($"Ответ от сервера: {response}");
    }
    catch (SocketException ex)
    {
        Console.WriteLine($"Ошибка сокета: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Общая ошибка: {ex.Message}");
    }
}