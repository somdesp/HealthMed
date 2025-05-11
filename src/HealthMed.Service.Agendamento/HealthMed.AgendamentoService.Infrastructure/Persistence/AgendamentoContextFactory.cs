using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.AgendamentoService.Infrastructure.Persistence;

public class AgendamentoContextFactory : IDesignTimeDbContextFactory<AgendamentoContext>
{
    public AgendamentoContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", true)
          .Build();

        var connectionString = configuration.GetConnectionString("AgendamentoConnectionString");

        var optionsBuilder = new DbContextOptionsBuilder<AgendamentoContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AgendamentoContext(optionsBuilder.Options);

    }
}
