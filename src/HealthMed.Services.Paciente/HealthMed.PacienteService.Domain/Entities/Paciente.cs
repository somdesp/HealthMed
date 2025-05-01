using HealthMed.BuildingBlocks.Common;
using HealthMed.BuildingBlocks.ValueObjects;

namespace HealthMed.PacienteService.Domain.Entities;

public class Paciente : EntityBase
{
    public required string Nome { get; set; }
    public CPF? Cpf { get; set; }
    public required string Senha { get; set; }
    public bool Ativo { get; set; }
}
