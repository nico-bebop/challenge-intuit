using Microsoft.AspNetCore.Mvc;
using TurnosMedicos.Application.Interfaces;

namespace TurnosMedicos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SucursalesController : ControllerBase
{
    private readonly ISucursalesService _service;

    public SucursalesController(ISucursalesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sucursales = await _service.GetAllAsync();
        return Ok(sucursales);
    }
}