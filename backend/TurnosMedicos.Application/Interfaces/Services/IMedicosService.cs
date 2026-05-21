using TurnosMedicos.Application.DTOs;

namespace TurnosMedicos.Application.Interfaces.Services;

public interface IMedicosService
{
    Task<List<MedicoDto>> GetAllAsync();
}