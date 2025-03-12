using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DeporteGest.Models;

namespace DeporteGest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string username = HttpContext.Session.GetString("Username") ?? string.Empty;
      ViewData["Username"] = username;  // Pasar el valor de la sesión a la vista

    if (string.IsNullOrEmpty(username))
    {
        return RedirectToAction("Login", "Users");
    }

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
