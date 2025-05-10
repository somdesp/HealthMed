using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HealthMed.AgendamentoService.Infrastructure.Persistence;

public class AgendamentoContextFactory : IDesignTimeDbContextFactory<AgendamentoContext>
{
    private readonly IConfiguration _configuration;

    public AgendamentoContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public AgendamentoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AgendamentoContext>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AgendamentoConnectionString"));

        return new AgendamentoContext(optionsBuilder.Options);

    }
}
