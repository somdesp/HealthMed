using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.MedicoService.Infrastructure.Persistence;

public class MedicoContextFactory : IDesignTimeDbContextFactory<MedicoContext>
{
    private readonly IConfiguration _configuration;

    public MedicoContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MedicoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MedicoContext>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MedicoConnectionString"));

        return new MedicoContext(optionsBuilder.Options);
    }
}
