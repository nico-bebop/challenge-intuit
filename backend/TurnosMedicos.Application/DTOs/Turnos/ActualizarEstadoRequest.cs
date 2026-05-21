using TurnosMedicos.Domain.Enums;

namespace TurnosMedicos.Application.DTOs.Turnos;

public class ActualizarEstadoRequest
{
    public EstadoTurno Estado { get; set; }
}
