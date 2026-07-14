using Core.Model;
using Core.ModelRequest;
using Core.ModelResponce;
using Lesson1;
using System.Text.Json;

while (true)
{
    Console.WriteLine("Введите запрос");

    var operation = Console.ReadLine();
    var operationNumber = int.Parse(operation);
    Request? request;

    if (operationNumber == 1)
    {
        request = new Request
        {
            TypeRequest = TypeRequest.Read,
            Body = string.Empty,
        };
    }
    else if (operationNumber == 2)
    {
        var product = new Product
        {
            Id = 0,
            Name = "Some Name",
            Description = "Some Name",
        };

        request = new Request
        {
            TypeRequest = TypeRequest.Create,
            Body = JsonSerializer.Serialize(product),
        };
    }
    else if (operationNumber == 3)
    {
        var product = new Product
        {
            Id = 2,
            Name = "Some Name1",
            Description = "Some Name1",
        };

        request = new Request
        {
            TypeRequest = TypeRequest.Update,
            Body = JsonSerializer.Serialize(product),
        };
    }
    else if (operationNumber == 4)
    {
        request = new Request
        {
            TypeRequest = TypeRequest.Delete,
            Body = "1",
        };
    }
    else
    {
        continue;
    }

    var requestJson = JsonSerializer.Serialize(request);
    using var client = new Client("127.0.0.1", 8888);
    await client.OpenConnectionAsync();
    await client.SendAsync(requestJson);
    var responceText = await client.ReciveAsync();
    var responceObj = JsonSerializer.Deserialize<Responce>(responceText);

    Console.WriteLine(responceObj.Body);
    if (responceObj.TypeResponse == TypeResponse.Read)
    {
        var products = JsonSerializer.Deserialize<List<Product>>(responceObj.Body);
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("TypeResponse.Read");
    }
    else if (responceObj.TypeResponse == TypeResponse.Create)
    {
        Console.WriteLine("TypeResponse.Create");
    }
    else if (responceObj.TypeResponse == TypeResponse.Update)
    {
        Console.WriteLine("TypeResponse.Update");
    }
    else if (responceObj.TypeResponse == TypeResponse.Delete)
    {
        Console.WriteLine("TypeResponse.Delete");
    }
}
