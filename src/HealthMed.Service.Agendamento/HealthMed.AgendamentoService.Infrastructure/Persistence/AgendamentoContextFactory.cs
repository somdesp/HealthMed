//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace HealthMed.AgendamentoService.Infrastructure.Persistence;

//public class AgendamentoContextFactory : IDesignTimeDbContextFactory<AgendamentoContext>
//{
//    public AgendamentoContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<AgendamentoContext>();
//        optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MedicoDb;Integrated Security=True;MultipleActiveResultSets=True");

//        return new AgendamentoContext(optionsBuilder.Options);
//    }
//}
