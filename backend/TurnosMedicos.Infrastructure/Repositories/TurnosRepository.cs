using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Application.Interfaces;
using TurnosMedicos.Domain.Enums;
using TurnosMedicos.Domain.Models;
using TurnosMedicos.Infrastructure.Data;

namespace TurnosMedicos.Infrastructure.Repositories;

public class TurnosRepository : ITurnosRepository
{
    private readonly AppDbContext _context;

    public TurnosRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Turno>> GetAllAsync()
        => _context.Turnos
            .Include(t => t.Paciente)
            .Include(t => t.Medico)
            .ToListAsync();

    public Task<Turno?> GetByIdAsync(int id)
        => _context.Turnos
            .Include(t => t.Paciente)
            .Include(t => t.Medico)
            .FirstOrDefaultAsync(t => t.Id == id);

    public Task AddAsync(Turno turno)
        => _context.Turnos.AddAsync(turno).AsTask();

    public Task<bool> ExisteConflictoAsync(int medicoId, DateTime fechaHora)
        => _context.Turnos.AnyAsync(t =>
            t.MedicoId == medicoId &&
            t.FechaHora == fechaHora &&
            t.Estado != EstadoTurno.Cancelado);

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();

}