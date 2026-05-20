using TurnosMedicos.Application.DTOs;

namespace TurnosMedicos.Application.Interfaces;

public interface ISucursalesService
{
    Task<List<SucursalDto>> GetAllAsync();
}