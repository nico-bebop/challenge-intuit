namespace TurnosMedicos.Application.DTOs.Turnos;

public class CreateTurnoRequest
{
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; }
}
