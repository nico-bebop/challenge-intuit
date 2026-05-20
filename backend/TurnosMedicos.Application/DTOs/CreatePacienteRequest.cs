namespace TurnosMedicos.Application.DTOs;

public class CreatePacienteRequest
{
    public string NombreCompleto { get; set; }
    public string DNI { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
}