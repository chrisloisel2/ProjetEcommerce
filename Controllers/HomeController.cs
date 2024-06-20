using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using exemple.Models;

namespace exemple.Controllers;

public class HomeController : Controller
{
    private readonly Logger<HomeController> _logger;

    public HomeController(Logger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(User user)
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
