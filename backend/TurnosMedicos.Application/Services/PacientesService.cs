using TurnosMedicos.Application.DTOs.Pacientes;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Application.Interfaces.Services;
using TurnosMedicos.Application.Mappers;

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

        return pacientes
            .Select(PacienteMapper.ToDto)
            .ToList();
    }

    public async Task<PacienteDto?> GetByIdAsync(int id)
    {
        var paciente = await _repo.GetByIdAsync(id);

        return paciente != null ? (PacienteDto?)PacienteMapper.ToDto(paciente) : null;
    }

    public async Task<PacienteDto> CreateAsync(CreatePacienteRequest request)
    {
        var paciente = PacienteMapper.ToEntity(request);

        await _repo.AddAsync(paciente);
        await _repo.SaveChangesAsync();

        return PacienteMapper.ToDto(paciente);
    }

    public async Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteRequest request)
    {
        var paciente = await _repo.GetByIdAsync(id);

        if (paciente == null)
            return null;

        PacienteMapper.UpdateEntity(paciente, request);

        await _repo.SaveChangesAsync();

        return PacienteMapper.ToDto(paciente);
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