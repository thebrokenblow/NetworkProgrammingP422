using Core.Model;
using Core.ModelResponce;
using Lesson2.Services.Interfaces;
using System.Text.Json;

namespace Lesson2.Services;

public class UpdateServices : IServices
{
    public Responce Execute(string requestBody)
    {
        var product = JsonSerializer.Deserialize<Product>(requestBody);
        //Обновил в бд

        var responce = new Responce
        {
            TypeResponse = TypeResponse.Update,
            Body = "Продукт успешно обновлён"
        };

        return responce;
    }
}
