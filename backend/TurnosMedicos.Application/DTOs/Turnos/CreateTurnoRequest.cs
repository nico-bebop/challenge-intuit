using System.ComponentModel.DataAnnotations;

namespace TurnosMedicos.Application.DTOs.Turnos;

public class CreateTurnoRequest
{
    [Required]
    public int PacienteId { get; set; }

    [Required]
    public int MedicoId { get; set; }

    [Required]
    public DateTime FechaHora { get; set; }

    [Required]
    [StringLength(150)]
    public string Motivo { get; set; } = string.Empty;
}