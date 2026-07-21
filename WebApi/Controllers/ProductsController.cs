using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = [];

    public ProductsController()
    {
        if (_products.Count == 0)
        {
            _products.Add(new Product
            {
                Id = 1,
                Name = "Some Name 1",
                Description = "Some Description 1",
            });

            _products.Add(new Product
            {
                Id = 2,
                Name = "Some Name 2",
                Description = "Some Description 2",
            });
        }
    }

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
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(_products.Remove(product));
    }
}
