using TurnosMedicos.Application.DTOs;
using TurnosMedicos.Application.Helpers;
using TurnosMedicos.Application.Interfaces;
using TurnosMedicos.Application.Mappers;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Services;

public class TurnosService : ITurnosService
{
    private readonly ITurnosRepository _turnosRepo;
    private readonly IPacientesRepository _pacientesRepo;

    public TurnosService(ITurnosRepository turnosRepo, IPacientesRepository pacientesRepo)
    {
        _turnosRepo = turnosRepo;
        _pacientesRepo = pacientesRepo;
    }

    public async Task<List<TurnoDto>> GetAllAsync()
    {
        var turnos = await _turnosRepo.GetAllAsync();

        return turnos
            .Select(TurnoMapper.ToDto)
            .ToList();
    }

    public async Task<TurnoDto> GetByIdAsync(int id)
    {
        var turno = await _turnosRepo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> CrearAsync(CreateTurnoRequest request)
    {
        var paciente = await _pacientesRepo.GetByIdAsync(request.PacienteId) ?? throw new Exception("Paciente no encontrado.");

        // Desbloqueo automático después de 30 días
        if (paciente.Bloqueado &&
            paciente.FechaBloqueo.HasValue &&
            paciente.FechaBloqueo <= DateTime.UtcNow)
        {
            paciente.Bloqueado = false;
            paciente.NoShowCount = 0;
            paciente.FechaBloqueo = null;

            await _pacientesRepo.SaveChangesAsync();
        }

        // Validación de bloqueo
        if (paciente.Bloqueado)
            throw new Exception($"Paciente bloqueado hasta {paciente.FechaBloqueo:dd/MM/yyyy}.");

        // Validación de conflicto de horario
        if (await _turnosRepo.ExisteConflictoAsync(request.MedicoId, request.FechaHora))
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

        await _turnosRepo.AddAsync(turno);
        await _turnosRepo.SaveChangesAsync();

        // Para que el mapper tenga acceso al nombre
        turno.Paciente = paciente;

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> CancelarAsync(int id)
    {
        var turno = await _turnosRepo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        if (turno.FechaHora - DateTime.UtcNow < TimeSpan.FromHours(24) &&
            turno.Paciente != null)
        {
            RegistrarAusencia(turno.Paciente);
        }

        turno.Estado = EstadoTurno.Cancelado;

        await _turnosRepo.SaveChangesAsync();

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> MarcarAusenciaAsync(int id)
    {
        var turno = await _turnosRepo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        if (!turno.FechaHora.IsWithinCancellationWindow())
            throw new Exception("Fuera de ventana válida.");

        turno.Estado = EstadoTurno.NoShow;

        if (turno.Paciente != null)
        {
            RegistrarAusencia(turno.Paciente);
        }

        await _turnosRepo.SaveChangesAsync();

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> ActualizarEstadoAsync(int id, ActualizarEstadoRequest request)
    {
        var turno = await _turnosRepo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        if (turno.Estado != EstadoTurno.NoShow &&
            request.Estado == EstadoTurno.NoShow &&
            turno.Paciente != null)
        {
            RegistrarAusencia(turno.Paciente);
        }

        turno.Estado = request.Estado;

        await _turnosRepo.SaveChangesAsync();

        return TurnoMapper.ToDto(turno);
    }

    private void RegistrarAusencia(Paciente paciente)
    {
        paciente.NoShowCount++;

        if (paciente.NoShowCount >= 3)
        {
            paciente.Bloqueado = true;
            paciente.FechaBloqueo = DateTime.UtcNow.AddDays(30);
        }
    }
}