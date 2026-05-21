using TurnosMedicos.Application.DTOs;

namespace TurnosMedicos.Application.Interfaces.Services;

public interface ISucursalesService
{
    Task<List<SucursalDto>> GetAllAsync();
}