using System.Net;
using System.Net.Sockets;
using System.Text;

var ipEndPoint = new IPEndPoint(IPAddress.Loopback, 8888);
var server = new Server(ipEndPoint, 1000);

Console.WriteLine("Сервер запущен");

while (true)
{
    var request = await server.ReceiveAsync();
    Console.WriteLine($"Запрос '{request}' получен");
    Console.WriteLine($"Отправляю ответ '{request}'");

    var numberRequest = int.Parse(request);

    if (IsPrime(numberRequest))
    {
        await server.SendAsync("Число простое");
    }
    else
    { 
        await server.SendAsync("Число составное");
    }
}

static bool IsPrime(int n)
{
    if (n <= 1) return false;
    if (n == 2) return true;
    if (n % 2 == 0) return false;

    var boundary = (int)Math.Floor(Math.Sqrt(n));

    for (int i = 3; i <= boundary; i += 2)
    {
        if (n % i == 0) return false;
    }

    return true;
}

public class Server : IDisposable
{
    private readonly Socket _serverSocket;
    private Socket? _clientSocket;

    private const int BufferSize = 1024;

    public Server(EndPoint endPoint, int backlog)
    {
        _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        _serverSocket.Bind(endPoint);
        _serverSocket.Listen(backlog);
    }

    public async Task<string> ReceiveAsync()
    {
        _clientSocket = await _serverSocket.AcceptAsync();
        var reques = await ReadResponceAsync(_clientSocket);

        return reques;
    }

    private async Task<string> ReadResponceAsync(Socket clientSocket)
    {
        int readBytes;
        var buffer = new byte[BufferSize];
        var requestBuilder = new StringBuilder();
        do
        {
            readBytes = await clientSocket.ReceiveAsync(buffer);
            var request = Encoding.UTF8.GetString(buffer, 0, readBytes);
            requestBuilder.Append(request);
        }
        while (readBytes > 0);

        return requestBuilder.ToString();
    }

    public async Task SendAsync(string responce)
    {
        if (_clientSocket != null)
        {
            var responceBytes = Encoding.UTF8.GetBytes(responce);
            await _clientSocket.SendAsync(responceBytes);

            _clientSocket.Dispose();
        }
    }

    public void Dispose()
    {
        _serverSocket.Dispose();
    }
}