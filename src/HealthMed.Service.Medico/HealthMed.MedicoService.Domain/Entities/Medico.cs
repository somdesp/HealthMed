using HealthMed.BuildingBlocks.Common;

namespace HealthMed.MedicoService.Domain.Entities;

public class Medico : EntityBase
{
    public required string Nome { get; set; }
    public required string Crm { get; set; }
    public required string Senha { get; set; }
    public required int ValorConsulta { get; set; }
    public bool Ativo { get; set; }

    public int EspecialidadeId { get; set; }
    public Especialidade? Especialidade { get; set; }
}
