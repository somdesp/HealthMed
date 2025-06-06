﻿// <auto-generated />
using System;
using HealthMed.PacienteService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthMed.PacienteService.Infrastructure.Migrations
{
    [DbContext(typeof(PacienteContext))]
    [Migration("20250428174851_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthMed.PacienteService.Domain.Entities.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("HealthMed.PacienteService.Domain.Entities.Paciente", b =>
                {
                    b.OwnsOne("HealthMed.BuildingBlocks.ValueObjects.CPF", "Cpf", b1 =>
                        {
                            b1.Property<int>("PacienteId")
                                .HasColumnType("int");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("nvarchar(11)")
                                .HasColumnName("Cpf");

                            b1.HasKey("PacienteId");

                            b1.ToTable("Pacientes");

                            b1.WithOwner()
                                .HasForeignKey("PacienteId");
                        });

                    b.Navigation("Cpf");
                });
#pragma warning restore 612, 618
        }
    }
}
