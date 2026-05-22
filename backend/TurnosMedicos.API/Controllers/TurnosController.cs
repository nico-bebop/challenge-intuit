using Microsoft.AspNetCore.Mvc;
using TurnosMedicos.Application.DTOs.Turnos;
using TurnosMedicos.Application.Interfaces.Services;

namespace TurnosMedicos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurnosController : ControllerBase
{
    private readonly ITurnosService _service;

    public TurnosController(ITurnosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var turnos = await _service.GetAllAsync();
        return Ok(turnos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var turno = await _service.GetByIdAsync(id);
        return turno == null ? NotFound() : Ok(turno);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateTurnoRequest request)
    {
        var turno = await _service.CrearAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = turno.Id }, turno);
    }

    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var turno = await _service.CancelarAsync(id);
        return Ok(turno);
    }

    [HttpPost("{id}/ausencia")]
    public async Task<IActionResult> Ausencia(int id)
    {
        var turno = await _service.MarcarAusenciaAsync(id);
        return Ok(turno);
    }

    [HttpPut("{id}/estado")]
    public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoRequest request)
    {
        var turno = await _service.ActualizarEstadoAsync(id, request);
        return turno == null ? NotFound() : Ok(turno);
    }
}