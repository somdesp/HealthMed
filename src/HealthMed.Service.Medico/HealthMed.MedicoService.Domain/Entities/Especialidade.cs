using HealthMed.BuildingBlocks.Common;

namespace HealthMed.MedicoService.Domain.Entities;

public class Especialidade : EntityBase
{
    public required string Nome { get; set; }

    public ICollection<Medico>? Medicos { get; set; }
}
