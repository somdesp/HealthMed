using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Infrastructure.Persistence;
using HealthMed.MedicoService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.MedicoService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MedicoContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MedicoConnectionString"));
        });
        services.AddScoped<IMedicoRepository, MedicoRepository>();

        return services;
    }
}
