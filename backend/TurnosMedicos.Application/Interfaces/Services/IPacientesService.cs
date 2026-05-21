using TurnosMedicos.Application.DTOs.Pacientes;

namespace TurnosMedicos.Application.Interfaces.Services;

public interface IPacientesService
{
    Task<List<PacienteDto>> GetAllAsync();
    Task<List<PacienteDto>> GetAllIncludingInactiveAsync();
    Task<PacienteDto?> GetByIdAsync(int id);
    Task<PacienteDto> CreateAsync(CreatePacienteRequest request);
    Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> ActivateAsync(int id);
}