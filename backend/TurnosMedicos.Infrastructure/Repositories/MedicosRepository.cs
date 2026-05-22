using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Domain.Models;
using TurnosMedicos.Infrastructure.Data;

namespace TurnosMedicos.Infrastructure.Repositories;

public class MedicosRepository : IMedicosRepository
{
    private readonly AppDbContext _context;

    public MedicosRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Medico>> GetAllAsync()
        => _context.Medicos
        .Include(m => m.Sucursal)
        .ToListAsync();
}