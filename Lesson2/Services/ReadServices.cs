using Core.Model;
using Core.ModelResponce;
using Lesson2.Services.Interfaces;
using System.Text.Json;

namespace Lesson2.Services;

public class ReadServices : IServices
{
    public Responce Execute(string requestBody)
    {
        var products = new List<Product>
            {
                new()
                {
                    Id = 1,
                    Name = "Iphone 17 pro max",
                    Description = "Телефон"
                },
                new()
                {
                    Id = 1,
                    Name = "Игрушка",
                    Description = "Игрушка"
                }
            };

        var responce = new Responce
        {
            TypeResponse = TypeResponse.Read,
            Body = JsonSerializer.Serialize(products)
        };

        return responce;
    }
}