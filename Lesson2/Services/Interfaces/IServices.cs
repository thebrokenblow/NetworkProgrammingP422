using Core.ModelResponce;

namespace Lesson2.Services.Interfaces;

public interface IServices
{
    Responce Execute(string requestBody);
}
