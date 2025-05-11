using HealthMed.BuildingBlocks.Common;
using HealthMed.MedicoService.Domain.Entities;

namespace HealthMed.MedicoServiceService.Domain.Entities;

public class AgendaMedico : EntityBase
{
    public DateTime DataHora { get; set; }
    public bool Reservada { get; set; }
    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }
}
