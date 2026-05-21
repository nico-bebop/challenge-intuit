using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Domain.Models;
using TurnosMedicos.Infrastructure.Data;

namespace TurnosMedicos.Infrastructure.Repositories;

public class PacientesRepository : IPacientesRepository
{
    private readonly AppDbContext _context;

    public PacientesRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Paciente>> GetAllAsync()
        => _context.Pacientes
            .Where(x => x.IsActive).ToListAsync();

    public Task<List<Paciente>> GetAllIncludingInactiveAsync()
        => _context.Pacientes.ToListAsync();

    public Task<Paciente?> GetByIdAsync(int id)
        => _context.Pacientes
            .FirstOrDefaultAsync(x => x.Id == id);

    public Task AddAsync(Paciente paciente)
        => _context.Pacientes.AddAsync(paciente).AsTask();

    public Task DeleteAsync(Paciente paciente)
    {
        paciente.IsActive = false;
        return Task.CompletedTask;
    }

    public Task ActivateAsync(Paciente paciente)
    {
        paciente.IsActive = true;
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}