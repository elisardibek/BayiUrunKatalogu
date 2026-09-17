using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BayiUrunKatalogu.Models;

namespace BayiUrunKatalogu.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
[Route("Home/HataSayfasi/{kod?}")]
public IActionResult HataSayfasi(int? kod)
{
    ViewData["HataKodu"] = kod ?? 500;
    return View();
}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
