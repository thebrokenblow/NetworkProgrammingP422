using ClientHttp.Model;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

await DeleteAsync();
await GetAsync();

async Task GetAsync()
{
    var httpClient = new HttpClient();
    var responceMessage = await httpClient.GetAsync("https://localhost:7227/Products");
    var responceJson = await responceMessage.Content.ReadAsStringAsync();
    var products = JsonSerializer.Deserialize<List<Product>>(responceJson);
}

async Task PostAsync()
{
    var product = new Product()
    {
        Id = 3,
        Name = "Some Name 3",
        Description = "Some Description 3",
    };

    var httpClient = new HttpClient();
    var responceMessage = await httpClient.PostAsJsonAsync("https://localhost:7227/Products", product);
}

async Task PutAsync()
{
    var product = new Product()
    {
        Id = 1,
        Name = "Some Name",
        Description = "Some Description",
    };

    var httpClient = new HttpClient();
    var responceMessage = await httpClient.PutAsJsonAsync("https://localhost:7227/Products", product);
}

async Task DeleteAsync()
{
    var httpClient = new HttpClient();
    var responceMessage = await httpClient.DeleteAsync("https://localhost:7227/Products/1");
}
