using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.MedicoService.Infrastructure.Persistence;

public class MedicoContextFactory : IDesignTimeDbContextFactory<MedicoContext>
{

    public MedicoContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true) // carrega o ambiente se existir
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<MedicoContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MedicoConnectionString"));

        return new MedicoContext(optionsBuilder.Options);
    }
}
