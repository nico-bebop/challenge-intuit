using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Interfaces.Repositories;

public interface ISucursalesRepository
{
    Task<List<Sucursal>> GetAllAsync();
}