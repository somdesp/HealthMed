using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.MedicoService.Infrastructure.Persistence;

public class MedicoContextFactory : IDesignTimeDbContextFactory<MedicoContext>
{
    public MedicoContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();

        var connectionString = configuration.GetConnectionString("MedicoConnectionString");
        var optionsBuilder = new DbContextOptionsBuilder<MedicoContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new MedicoContext(optionsBuilder.Options);
    }
}
