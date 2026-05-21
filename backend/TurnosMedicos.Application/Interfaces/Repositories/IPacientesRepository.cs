using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Interfaces.Repositories;

public interface IPacientesRepository
{
    Task<List<Paciente>> GetAllAsync();
    Task<Paciente?> GetByIdAsync(int id);
    Task AddAsync(Paciente paciente);
    Task DeleteAsync(Paciente paciente);
    Task SaveChangesAsync();
}