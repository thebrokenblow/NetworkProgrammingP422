using Core.Model;
using System.Text;
using System.Text.Json;

var product = new Product()
{
    Id = 1,
    Name = "Test",
    Description = "Test",
};

var jsonProduct = JsonSerializer.Serialize(product);
var bytes = Encoding.UTF8.GetBytes(jsonProduct);

//

var textObjectFromClient = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
var productFromClient = JsonSerializer.Deserialize<Product>(textObjectFromClient);

Console.ReadLine();