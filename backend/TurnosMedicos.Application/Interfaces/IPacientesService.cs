using TurnosMedicos.Application.DTOs;

namespace TurnosMedicos.Application.Interfaces;

public interface IPacientesService
{
    Task<List<PacienteDto>> GetAllAsync();
    Task<PacienteDto?> GetByIdAsync(int id);
    Task<PacienteDto> CreateAsync(CreatePacienteRequest request);
    Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteRequest request);
    Task<bool> DeleteAsync(int id);
}