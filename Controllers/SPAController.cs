using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CgnClean.Models;

namespace CgnClean.Controllers;

public class SPAController : Controller
{
    private readonly ILogger<SPAController> _logger;

    public SPAController(ILogger<SPAController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}

