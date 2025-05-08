using HealthMed.AgendamentoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.AgendamentoService.Infrastructure.Persistence;

public class AgendamentoContext : DbContext
{
    public AgendamentoContext(DbContextOptions<AgendamentoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgendamentoContext).Assembly);
    }

    public DbSet<Agendamento> Agendamentos { get; set; }
}
