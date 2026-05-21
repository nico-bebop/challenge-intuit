using TurnosMedicos.Application.DTOs.Sucursales;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Mappers;

public static class SucursalMapper
{
    public static SucursalDto ToDto(Sucursal sucursal)
    {
        return new SucursalDto
        {
            Id = sucursal.Id,
            Nombre = sucursal.Nombre,
            Direccion = sucursal.Direccion
        };
    }
}