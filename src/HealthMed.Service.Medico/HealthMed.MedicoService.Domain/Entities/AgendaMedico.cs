using HealthMed.BuildingBlocks.Common;

namespace HealthMed.MedicoService.Domain.Entities;

public class AgendaMedico : EntityBase
{
    public DateTime DataHora { get; set; }
    public bool Reservada { get; set; }
    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }
}
