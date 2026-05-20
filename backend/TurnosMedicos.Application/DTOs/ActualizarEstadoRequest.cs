using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.DTOs;

public class ActualizarEstadoRequest
{
    public EstadoTurno Estado { get; set; }
}
