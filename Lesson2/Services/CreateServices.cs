using Core.Model;
using Core.ModelResponce;
using Lesson2.Services.Interfaces;
using System.Text.Json;

namespace Lesson2.Services;

public class CreateServices : IServices
{
    public Responce Execute(string requestBody)
    {
        var product = JsonSerializer.Deserialize<Product>(requestBody);
        //Записал в бд

        var responce = new Responce
        {
            TypeResponse = TypeResponse.Create,
            Body = "Продукт успешно создан"
        };

        return responce;
    }
}
