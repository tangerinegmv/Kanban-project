using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_proyecto_2024_tangerinegmv.Models;

namespace tl2_proyecto_2024_tangerinegmv.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Login");
        return View();
    }

    public IActionResult Privacy()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Login");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
