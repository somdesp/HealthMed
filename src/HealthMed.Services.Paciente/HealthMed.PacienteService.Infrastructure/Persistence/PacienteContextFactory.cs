using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.PacienteService.Infrastructure.Persistence;

public class PacienteContextFactory : IDesignTimeDbContextFactory<PacienteContext>
{
    public PacienteContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true)
        .Build();

        var connectionString = configuration.GetConnectionString("PacienteConnectionString");
        var optionsBuilder = new DbContextOptionsBuilder<PacienteContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new PacienteContext(optionsBuilder.Options);
    }
}