using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.MedicoService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_BD_Medico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Crm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorConsulta = table.Column<double>(type: "float", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    EspecialidadeId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicos_Especialidades_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgendaMedico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reservada = table.Column<bool>(type: "bit", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaMedico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaMedico_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaMedico_MedicoId",
                table: "AgendaMedico",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_EspecialidadeId",
                table: "Medicos",
                column: "EspecialidadeId");

            migrationBuilder.Sql(@$"INSERT INTO Especialidades (Nome,DataCriacao) VALUES
                            ('Clínico Geral',GetDate()),
                            ('Cardiologia',GetDate()),
                            ('Dermatologia',GetDate()),
                            ('Pediatria',GetDate()),
                            ('Ortopedia',GetDate());");

            migrationBuilder.Sql(@$"INSERT INTO Medicos (Nome, EspecialidadeId, Senha, Crm, ValorConsulta, Ativo,DataCriacao) VALUES
                            ('Dra. Ana Souza', 1, 'dIMkGdpIuy0uLoqHOSC3Bw==', '12345',300, 1,GetDate()),
                            ('Dr. Bruno Lima', 2, 'dIMkGdpIuy0uLoqHOSC3Bw==', '23456',500, 1,GetDate()),
                            ('Dra. Camila Ribeiro', 3, 'dIMkGdpIuy0uLoqHOSC3Bw==', '34567',600, 1,GetDate()),
                            ('Dr. Daniel Almeida', 4, 'dIMkGdpIuy0uLoqHOSC3Bw==', '45678',350, 1,GetDate()),
                            ('Dra. Elisa Martins', 5, 'dIMkGdpIuy0uLoqHOSC3Bw==', '56789',920, 1,GetDate()),
                            ('Dr. Felipe Costa', 1, 'dIMkGdpIuy0uLoqHOSC3Bw==', '67890',410, 1,GetDate()),
                            ('Dra. Gabriela Nunes', 2, 'dIMkGdpIuy0uLoqHOSC3Bw==', '78901',280, 1,GetDate()),
                            ('Dr. Henrique Prado', 3, 'dIMkGdpIuy0uLoqHOSC3Bw==', '89012',320, 1,GetDate()),
                            ('Dra. Isabela Tavares', 4, 'dIMkGdpIuy0uLoqHOSC3Bw==', '90123',2500, 1,GetDate()),
                            ('Dr. João Mendes', 5, 'dIMkGdpIuy0uLoqHOSC3Bw==', '01234',650, 1,GetDate());");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaMedico");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Especialidades");
        }
    }
}
