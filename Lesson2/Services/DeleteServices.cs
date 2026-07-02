using Core.ModelResponce;
using Lesson2.Services.Interfaces;
using System.Text.Json;

namespace Lesson2.Services;

public class DeleteServices : IServices
{
    public Responce Execute(string requestBody)
    {
        var idProduct = JsonSerializer.Deserialize<int>(requestBody);
        //Удалю из бд по Id

        var responce = new Responce
        {
            TypeResponse = TypeResponse.Delete,
            Body = "Продукт успешно удалён"
        };

        return responce;
    }
}
