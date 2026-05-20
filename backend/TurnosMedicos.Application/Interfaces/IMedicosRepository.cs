using TurnosMedicos.Domain.Models;

namespace TurnosMedicos.Application.Interfaces;

public interface IMedicosRepository
{
    Task<List<Medico>> GetAllAsync();
}