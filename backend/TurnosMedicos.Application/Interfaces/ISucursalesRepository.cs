using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Interfaces;

public interface ISucursalesRepository
{
    Task<List<Sucursal>> GetAllAsync();
}