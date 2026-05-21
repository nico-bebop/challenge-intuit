using TurnosMedicos.Application.DTOs;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Mappers;

public static class PacienteMapper
{
    public static PacienteDto ToDto(Paciente paciente)
    {
        return new PacienteDto
        {
            Id = paciente.Id,
            NombreCompleto = paciente.NombreCompleto,
            DNI = paciente.DNI,
            Email = paciente.Email,
            Telefono = paciente.Telefono
        };
    }

    public static Paciente ToEntity(CreatePacienteRequest request)
    {
        return new Paciente
        {
            NombreCompleto = request.NombreCompleto,
            DNI = request.DNI,
            Email = request.Email,
            Telefono = request.Telefono,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    public static void UpdateEntity(
        Paciente paciente,
        UpdatePacienteRequest request)
    {
        paciente.NombreCompleto = request.NombreCompleto;
        paciente.DNI = request.DNI;
        paciente.Email = request.Email;
        paciente.Telefono = request.Telefono;
    }
}