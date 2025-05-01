using HealthMed.BuildingBlocks.ValueObjects;
using HealthMed.PacienteService.Domain.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;

public class PacienteMap : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(p => p.Cpf, cpf =>
        {
            cpf.Property(c => c.Numero)
                .HasColumnName("Cpf")
                .HasMaxLength(11)
                .IsRequired();
        });

        builder.Property(p => p.Senha)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Ativo)
            .IsRequired();

    }
}
