using Microsoft.Extensions.Options;
using TurnosMedicos.Application.DTOs;
using TurnosMedicos.Application.Helpers;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Application.Interfaces.Services;
using TurnosMedicos.Application.Mappers;
using TurnosMedicos.Application.Settings;
using TurnosMedicos.Domain.Enums;
using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Services;

public class TurnosService : ITurnosService
{
    private readonly ITurnosRepository _turnosRepo;
    private readonly IPacientesRepository _pacientesRepo;
    private readonly BusinessRulesSettings _rules;

    public TurnosService(ITurnosRepository turnosRepo, IPacientesRepository pacientesRepo, IOptions<BusinessRulesSettings> rules)
    {
        _turnosRepo = turnosRepo;
        _pacientesRepo = pacientesRepo;
        _rules = rules.Value;
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
        var paciente = await _pacientesRepo.GetByIdAsync(request.PacienteId)
            ?? throw new Exception("Paciente no encontrado.");

        ValidarBloqueoPaciente(paciente);

        // Validación de conflicto de horario
        if (await _turnosRepo.ExisteConflictoAsync(request.MedicoId, request.FechaHora))
            throw new Exception("Conflicto de horario.");

        var turno = TurnoMapper.ToEntity(request);

        await _turnosRepo.AddAsync(turno);
        await _turnosRepo.SaveChangesAsync();

        turno.Paciente = paciente;

        return TurnoMapper.ToDto(turno);
    }

    public async Task<TurnoDto> CancelarAsync(int id)
    {
        var turno = await _turnosRepo.GetByIdAsync(id)
            ?? throw new Exception("Turno no encontrado.");

        if (turno.FechaHora - DateTime.UtcNow < TimeSpan.FromHours(_rules.CancellationWindowHours) &&
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

        if (paciente.NoShowCount >= _rules.NoShowLimit)
        {
            paciente.Bloqueado = true;
            paciente.FechaBloqueo = DateTime.UtcNow.AddDays(_rules.BlockDays);
        }
    }

    private void ValidarBloqueoPaciente(Paciente paciente)
    {
        // Desbloqueo automático
        if (paciente.Bloqueado &&
            paciente.FechaBloqueo.HasValue &&
            paciente.FechaBloqueo <= DateTime.UtcNow)
        {
            paciente.Bloqueado = false;
            paciente.NoShowCount = 0;
            paciente.FechaBloqueo = null;
        }

        // Sigue bloqueado
        if (paciente.Bloqueado)
        {
            throw new Exception(
                $"Paciente bloqueado hasta {paciente.FechaBloqueo:dd/MM/yyyy}.");
        }
    }
}