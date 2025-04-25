using HealthMed.PacienteService.Application.Contracts.Persistence;
using HealthMed.PacienteService.Infrastructure.Persistence;
using HealthMed.PacienteService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.PacienteService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PacienteContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MedicoConnectionString"));
        });
        services.AddScoped<IPacienteRepository, PacienteRepository>();

        return services;
    }
}
