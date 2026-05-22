using TurnosMedicos.Application.DTOs.Medicos;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Mappers;

public static class MedicoMapper
{
    public static MedicoDto ToDto(Medico medico)
    {
        return new MedicoDto
        {
            Id = medico.Id,
            NombreCompleto = medico.NombreCompleto,
            Especialidad = medico.Especialidad,
            SucursalId = medico.SucursalId,
            SucursalNombre = medico.Sucursal?.Nombre
        };
    }
}