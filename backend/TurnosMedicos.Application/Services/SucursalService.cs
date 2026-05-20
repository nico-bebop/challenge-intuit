using TurnosMedicos.Application.DTOs;
using TurnosMedicos.Application.Interfaces;

namespace TurnosMedicos.Application.Services;

public class SucursalesService : ISucursalesService
{
    private readonly ISucursalesRepository _repo;

    public SucursalesService(ISucursalesRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SucursalDto>> GetAllAsync()
    {
        var sucursales = await _repo.GetAllAsync();

        return sucursales.Select(s => new SucursalDto
        {
            Id = s.Id,
            Nombre = s.Nombre,
            Direccion = s.Direccion
        }).ToList();
    }
}

