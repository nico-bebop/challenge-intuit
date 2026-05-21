using Microsoft.AspNetCore.Mvc;
using TurnosMedicos.Application.DTOs;
using TurnosMedicos.Application.Interfaces.Services;

namespace TurnosMedicos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacientesService _service;

    public PacientesController(IPacientesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pacientes = await _service.GetAllAsync();
        return Ok(pacientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var paciente = await _service.GetByIdAsync(id);
        return paciente == null ? NotFound() : Ok(paciente);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePacienteRequest request)
    {
        var created = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePacienteRequest request)
    {
        var updated = await _service.UpdateAsync(id, request);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}