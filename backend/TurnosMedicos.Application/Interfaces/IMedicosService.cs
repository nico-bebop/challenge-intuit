using TurnosMedicos.Application.DTOs;

namespace TurnosMedicos.Application.Interfaces;

public interface IMedicosService
{
    Task<List<MedicoDto>> GetAllAsync();
}