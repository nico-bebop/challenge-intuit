using TurnosMedicos.Application.DTOs.Medicos;

namespace TurnosMedicos.Application.Interfaces.Services;

public interface IMedicosService
{
    Task<List<MedicoDto>> GetAllAsync();
}