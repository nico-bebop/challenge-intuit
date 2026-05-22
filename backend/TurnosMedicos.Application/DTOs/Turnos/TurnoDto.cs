namespace TurnosMedicos.Application.DTOs.Turnos;

public class TurnoDto
{
    public int Id { get; set; }
    public int? PacienteId { get; set; }
    public string? PacienteNombre { get; set; }
    public string? PacienteDNI { get; set; }
    public int MedicoId { get; set; }
    public string? MedicoNombre { get; set; }
    public string? Especialidad { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public string Motivo { get; set; } = string.Empty;
}