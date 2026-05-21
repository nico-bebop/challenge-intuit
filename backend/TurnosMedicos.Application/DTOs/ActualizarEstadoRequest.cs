using TurnosMedicos.Domain.Enums;

namespace TurnosMedicos.Application.DTOs;

public class ActualizarEstadoRequest
{
    public EstadoTurno Estado { get; set; }
}
