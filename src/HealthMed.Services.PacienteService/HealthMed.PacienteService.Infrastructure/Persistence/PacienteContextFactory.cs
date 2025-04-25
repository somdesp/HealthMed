using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.PacienteService.Infrastructure.Persistence;

public class PacienteContextFactory : IDesignTimeDbContextFactory<PacienteContext>
{
    private readonly IConfiguration _configuration;

    public PacienteContextFactory()
    {
        
    }

    public PacienteContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public PacienteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PacienteContext>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PacienteConnectionString"));

        return new PacienteContext(optionsBuilder.Options);
    }
}