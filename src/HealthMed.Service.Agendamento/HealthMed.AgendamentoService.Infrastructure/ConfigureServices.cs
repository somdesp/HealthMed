using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Infrastructure.Persistence;
using HealthMed.AgendamentoService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.AgendamentoService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AgendamentoContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AgendamentoConnectionString"));
        });
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

        return services;
    }
}
