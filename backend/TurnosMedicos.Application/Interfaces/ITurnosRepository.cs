using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Interfaces;

public interface ITurnosRepository
{
    Task<List<Turno>> GetAllAsync();
    Task<Turno?> GetByIdAsync(int id);
    Task AddAsync(Turno turno);
    Task<bool> ExisteConflictoAsync(int medicoId, DateTime fechaHora);
    Task SaveChangesAsync();
}
