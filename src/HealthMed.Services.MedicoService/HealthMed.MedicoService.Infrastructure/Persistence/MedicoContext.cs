using HealthMed.MedicoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.MedicoService.Infrastructure.Persistence;

public class MedicoContext : DbContext
{
    public MedicoContext(DbContextOptions<MedicoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicoContext).Assembly);
    }

    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
}
