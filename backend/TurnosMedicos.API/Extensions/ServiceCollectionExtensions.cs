using TurnosMedicos.Application.Interfaces.Repositories;
using TurnosMedicos.Application.Interfaces.Services;
using TurnosMedicos.Application.Services;
using TurnosMedicos.Infrastructure.Repositories;

namespace TurnosMedicos.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ITurnosService, TurnosService>();
        services.AddScoped<ITurnosRepository, TurnosRepository>();

        services.AddScoped<IMedicosService, MedicosService>();
        services.AddScoped<IMedicosRepository, MedicosRepository>();

        services.AddScoped<IPacientesService, PacientesService>();
        services.AddScoped<IPacientesRepository, PacientesRepository>();

        services.AddScoped<ISucursalesService, SucursalesService>();
        services.AddScoped<ISucursalesRepository, SucursalesRepository>();

        return services;
    }
}