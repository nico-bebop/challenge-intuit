using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Domain.Models;
using TurnosMedicos.Infrastructure.Data;

namespace TurnosMedicos.Infrastructure.Repositories;

public class SucursalesRepository : ISucursalesRepository
{
    private readonly AppDbContext _context;

    public SucursalesRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Sucursal>> GetAllAsync()
        => _context.Sucursales.ToListAsync();
}