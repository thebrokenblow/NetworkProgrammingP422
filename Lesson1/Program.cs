using Lesson1;

const int _port = 80;
const string _url = "www.google.com";

var client = new Client(_url, _port);
await client.ConnectionAsync();
await client.SendAsync($"GET / HTTP/1.1\r\nHost: {_url}\r\nConnection: Close\r\n\r\n");
var responce = await client.ReadAsync();

Console.WriteLine(responce);