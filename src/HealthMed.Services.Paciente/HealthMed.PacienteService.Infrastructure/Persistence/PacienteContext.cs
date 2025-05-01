using HealthMed.PacienteService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.PacienteService.Infrastructure.Persistence;

public class PacienteContext : DbContext
{
    public PacienteContext(DbContextOptions<PacienteContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PacienteContext).Assembly);
    }

    public DbSet<Paciente> Pacientes { get; set; }
}

