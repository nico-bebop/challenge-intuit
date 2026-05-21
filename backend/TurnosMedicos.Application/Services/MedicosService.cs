using TurnosMedicos.Application.DTOs.Medicos;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Application.Interfaces.Services;

namespace TurnosMedicos.Application.Services;

public class MedicosService : IMedicosService
{
    private readonly IMedicosRepository _repo;

    public MedicosService(IMedicosRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<MedicoDto>> GetAllAsync()
    {
        var medicos = await _repo.GetAllAsync();

        return medicos.Select(m => new MedicoDto
        {
            Id = m.Id,
            NombreCompleto = m.NombreCompleto,
            Especialidad = m.Especialidad,
            SucursalId = m.SucursalId,
            SucursalNombre = m.Sucursal?.Nombre
        }).ToList();
    }
}