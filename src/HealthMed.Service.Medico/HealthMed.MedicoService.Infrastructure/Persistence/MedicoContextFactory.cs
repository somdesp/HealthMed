using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HealthMed.MedicoService.Infrastructure.Persistence;

public class MedicoContextFactory : IDesignTimeDbContextFactory<MedicoContext>
{
    public MedicoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MedicoContext>();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MedicoDb;Integrated Security=True;MultipleActiveResultSets=True");

        return new MedicoContext(optionsBuilder.Options);
    }
}
