using System.Net;
using System.Net.Sockets;
using System.Text;

const int Port = 11000;
const int BufferSize = 1024;

using var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
serverSocket.Bind(new IPEndPoint(IPAddress.Any, Port));

Console.WriteLine($"UDP-сервер запущен на порту {Port}. Ожидание сообщений...");

while (true)
{
    try
    {
        byte[] buffer = new byte[BufferSize];
        EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

        SocketReceiveFromResult result = await serverSocket.ReceiveFromAsync(
            buffer,
            SocketFlags.None,
            clientEndPoint);

        int bytesReceived = result.ReceivedBytes;
        EndPoint remoteEndPoint = result.RemoteEndPoint;

        await ProcessMessageAsync(serverSocket, buffer, bytesReceived, remoteEndPoint);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при приёме: {ex.Message}");
    }
}

static async Task ProcessMessageAsync(Socket socket, byte[] buffer, int bytesReceived, EndPoint clientEndPoint)
{
    try
    {
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
        Console.WriteLine($"Получено сообщение от {clientEndPoint}: {receivedMessage}");

        string response = $"Эхо: {receivedMessage}";
        byte[] sendBuffer = Encoding.UTF8.GetBytes(response);

        await socket.SendToAsync(
            sendBuffer,
            SocketFlags.None,
            clientEndPoint);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при обработке сообщения от {clientEndPoint}: {ex.Message}");
    }
}