using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Interfaces.Repositories;

public interface IMedicosRepository
{
    Task<List<Medico>> GetAllAsync();
}