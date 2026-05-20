using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Application.Interfaces;
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
        => _context.Pacientes.ToListAsync();

    public Task<Paciente?> GetByIdAsync(int id)
        => _context.Pacientes.FirstOrDefaultAsync(x => x.Id == id);

    public Task AddAsync(Paciente paciente)
        => _context.Pacientes.AddAsync(paciente).AsTask();

    public Task DeleteAsync(Paciente paciente)
    {
        _context.Pacientes.Remove(paciente);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}