using TurnosMedicos.Application.DTOs;
using TurnosMedicos.Application.Helpers;
using TurnosMedicos.Application.Interfaces;
using TurnosMedicos.Application.Mappers;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Services;

public class TurnosService : ITurnosService
{
    private readonly ITurnosRepository _repo;

    public TurnosService(ITurnosRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TurnoDto>> GetAllAsync()
    {
        var turnos = await _repo.GetAllAsync();

        return turnos
            .Select(TurnoMapper.ToDto)
            .ToList();
    }

    public async Task<TurnoDto> GetByIdAsync(int id)
    {
        var turno = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> CrearAsync(CreateTurnoRequest request)
    {
        if (await _repo.ExisteConflictoAsync(request.MedicoId, request.FechaHora))
            throw new Exception("Conflicto de horario.");

        var turno = new Turno
        {
            PacienteId = request.PacienteId,
            MedicoId = request.MedicoId,
            FechaHora = request.FechaHora,
            FechaCreacion = DateTime.UtcNow,
            Estado = EstadoTurno.Pendiente,
            Motivo = request.Motivo
        };

        await _repo.AddAsync(turno);
        await _repo.SaveChangesAsync();

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> CancelarAsync(int id)
    {
        var turno = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        if (turno.FechaHora - DateTime.UtcNow < TimeSpan.FromHours(24))
            throw new Exception("No se puede cancelar dentro de 24h.");

        turno.Estado = EstadoTurno.Cancelado;

        await _repo.SaveChangesAsync();

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> MarcarAusenciaAsync(int id)
    {
        var turno = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        if (!turno.FechaHora.IsWithinCancellationWindow())
            throw new Exception("Fuera de ventana válida.");

        turno.Estado = EstadoTurno.NoShow;

        await _repo.SaveChangesAsync();

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> ActualizarEstadoAsync(int id, ActualizarEstadoRequest request)
    {
        var turno = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        turno.Estado = request.Estado;

        await _repo.SaveChangesAsync();

        return TurnoMapper.ToDto(turno);
    }
}