using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = [];

    [HttpGet]
    public List<Product> Get()
    {
        return _products;
    }

    [HttpPost]
    public void Post([FromBody] Product product)
    {
        _products.Add(product);
    }

    [HttpPut]
    public void Put([FromBody] Product product)
    {
        var updatingProduct = _products.First(x => x.Id == product.Id);

        updatingProduct.Name = product.Name;
        updatingProduct.Description = product.Description;
    }

    [HttpDelete("{id:int}")]
    public void Delete(int id)
    {
        var product = _products.First(x => x.Id == id);

        _products.Remove(product);
    }
}
