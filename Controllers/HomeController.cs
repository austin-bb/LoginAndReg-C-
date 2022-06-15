using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginAndReg.Models;

namespace LoginAndReg.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private int? uid
    {
      get
      {
        return HttpContext.Session.GetInt32("UUID");
      }
    }

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return RedirectToAction("Registration", "Users");
    }

    [HttpGet("/success")]
    public IActionResult Success()
    {
      return View("Success");
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
