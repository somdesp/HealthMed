using HealthMed.BuildingBlocks.Common;
using HealthMed.MedicoServiceService.Domain.Entities;

namespace HealthMed.MedicoService.Domain.Entities;

public class Medico : EntityBase
{
    public required string Nome { get; set; }
    public required string Crm { get; set; }
    public required string Senha { get; set; }
    public required double ValorConsulta { get; set; }
    public bool Ativo { get; set; }

    public int EspecialidadeId { get; set; }
    public Especialidade? Especialidade { get; set; }
    public ICollection<AgendaMedico>? AgendaMedico { get; set; }
}
