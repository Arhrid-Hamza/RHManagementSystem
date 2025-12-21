using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RHManagementSystem.Models;

namespace RHManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var userRole = HttpContext.Session.GetString("UserRole");
        if (!string.IsNullOrEmpty(userRole))
        {
            if (userRole == "admin")
            {
                return RedirectToAction("Index", "AdminDashboard");
            }
            else
            {
                return RedirectToAction("Index", "UserDashboard");
            }
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
