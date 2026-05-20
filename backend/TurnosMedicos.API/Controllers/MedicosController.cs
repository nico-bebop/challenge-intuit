using Microsoft.AspNetCore.Mvc;
using TurnosMedicos.Application.Interfaces;

namespace TurnosMedicos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicosController : ControllerBase
{
    private readonly IMedicosService _service;

    public MedicosController(IMedicosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var medicos = await _service.GetAllAsync();
        return Ok(medicos);
    }
}