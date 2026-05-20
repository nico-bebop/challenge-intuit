namespace TurnosMedicos.Application.DTOs;

public class MedicoDto
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; }
    public string Especialidad { get; set; }
    public int SucursalId { get; set; }
    public string? SucursalNombre { get; set; }
}