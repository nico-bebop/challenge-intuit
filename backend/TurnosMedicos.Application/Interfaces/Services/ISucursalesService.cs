using TurnosMedicos.Application.DTOs.Sucursales;

namespace TurnosMedicos.Application.Interfaces.Services;

public interface ISucursalesService
{
    Task<List<SucursalDto>> GetAllAsync();
}