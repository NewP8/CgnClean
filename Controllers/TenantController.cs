using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CgnClean.Models;
using CgnClean.CgnFintech.Application.Commands;
using MediatR;

namespace CgnClean.Controllers;

public class TenantController : Controller
{
    private readonly ILogger<TenantController> _logger;
    private readonly IMediator _mediator;

    public TenantController(
        ILogger<TenantController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreaTenant()
    {
        _logger.LogInformation("Creazione nuovo tenant");
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreaTenant([FromForm] string nome)
    {
        _logger.LogInformation(
            "----- Sending command");

        var t =  await _mediator.Send(new CreaTenant{
            TenantName = nome,
        });
        return t;
    }
}
