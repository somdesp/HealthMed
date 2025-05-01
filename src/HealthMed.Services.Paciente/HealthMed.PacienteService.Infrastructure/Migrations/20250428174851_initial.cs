using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.PacienteService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.Sql(@$"INSERT INTO Pacientes (Nome, Senha, Ativo,Cpf,DataCriacao)
                        VALUES
                        ('João Silva', 'dIMkGdpIuy0uLoqHOSC3Bw==', 1,'02268881008',GetDate()),
                        ('Maria Oliveira', 'dIMkGdpIuy0uLoqHOSC3Bw==', 1,'04052748077',GetDate()),
                        ('Carlos Souza', 'dIMkGdpIuy0uLoqHOSC3Bw==', 1,'15960236001',GetDate()),
                        ('Ana Lima', 'dIMkGdpIuy0uLoqHOSC3Bw==', 1,'41326936000',GetDate()),
                        ('Pedro Santos', 'dIMkGdpIuy0uLoqHOSC3Bw==', 1,'85287380003',GetDate());");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
