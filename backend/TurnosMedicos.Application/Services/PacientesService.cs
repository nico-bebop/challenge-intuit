using TurnosMedicos.Application.DTOs;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Application.Interfaces.Services;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Services;

public class PacientesService : IPacientesService
{
    private readonly IPacientesRepository _repo;

    public PacientesService(IPacientesRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<PacienteDto>> GetAllAsync()
    {
        var pacientes = await _repo.GetAllAsync();

        return pacientes.Select(p => new PacienteDto
        {
            Id = p.Id,
            NombreCompleto = p.NombreCompleto,
            DNI = p.DNI,
            Email = p.Email,
            Telefono = p.Telefono
        }).ToList();
    }

    public async Task<PacienteDto?> GetByIdAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return null;

        return new PacienteDto
        {
            Id = p.Id,
            NombreCompleto = p.NombreCompleto,
            DNI = p.DNI,
            Email = p.Email,
            Telefono = p.Telefono
        };
    }

    public async Task<PacienteDto> CreateAsync(CreatePacienteRequest request)
    {
        var paciente = new Paciente
        {
            NombreCompleto = request.NombreCompleto,
            DNI = request.DNI,
            Email = request.Email,
            Telefono = request.Telefono,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _repo.AddAsync(paciente);
        await _repo.SaveChangesAsync();

        return new PacienteDto
        {
            Id = paciente.Id,
            NombreCompleto = paciente.NombreCompleto,
            DNI = paciente.DNI,
            Email = paciente.Email,
            Telefono = paciente.Telefono
        };
    }

    public async Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteRequest request)
    {
        var paciente = await _repo.GetByIdAsync(id);
        if (paciente == null) return null;

        paciente.NombreCompleto = request.NombreCompleto;
        paciente.DNI = request.DNI;
        paciente.Email = request.Email;
        paciente.Telefono = request.Telefono;

        await _repo.SaveChangesAsync();

        return new PacienteDto
        {
            Id = paciente.Id,
            NombreCompleto = paciente.NombreCompleto,
            DNI = paciente.DNI,
            Email = paciente.Email,
            Telefono = paciente.Telefono
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var paciente = await _repo.GetByIdAsync(id);
        if (paciente == null) return false;

        await _repo.DeleteAsync(paciente);
        await _repo.SaveChangesAsync();

        return true;
    }
}