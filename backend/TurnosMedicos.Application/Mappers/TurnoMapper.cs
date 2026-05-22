using TurnosMedicos.Application.DTOs.Turnos;
using TurnosMedicos.Domain.Enums;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Mappers;

public static class TurnoMapper
{
    public static TurnoDto ToDto(Turno t)
    {
        return new TurnoDto
        {
            Id = t.Id,
            PacienteId = t.PacienteId,
            PacienteNombre = t.Paciente?.NombreCompleto,
            PacienteDNI = t.Paciente?.DNI,
            MedicoId = t.MedicoId,
            MedicoNombre = t.Medico?.NombreCompleto,
            Especialidad = t.Medico?.Especialidad,
            FechaHora = t.FechaHora,
            Estado = t.Estado.ToString(),
            FechaCreacion = t.FechaCreacion,
            Motivo = t.Motivo
        };
    }

    public static Turno ToEntity(CreateTurnoRequest request)
    {
        return new Turno
        {
            PacienteId = request.PacienteId,
            MedicoId = request.MedicoId,
            FechaHora = request.FechaHora,
            FechaCreacion = DateTime.UtcNow,
            Estado = EstadoTurno.Pendiente,
            Motivo = request.Motivo
        };
    }
}