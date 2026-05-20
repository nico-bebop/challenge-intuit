using TurnosMedicos.Application.DTOs;

namespace TurnosMedicos.Application.Interfaces;

public interface ITurnosService
{
    Task<List<TurnoDto>> GetAllAsync();
    Task<TurnoDto?> GetByIdAsync(int id);
    Task<TurnoDto> CrearAsync(CreateTurnoRequest request);
    Task<TurnoDto> CancelarAsync(int id);
    Task<TurnoDto> MarcarAusenciaAsync(int id);
    Task<TurnoDto> ActualizarEstadoAsync(int id, ActualizarEstadoRequest request);
}
