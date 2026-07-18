using Microsoft.AspNetCore.Mvc;

namespace HttpServerSideRendering.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
