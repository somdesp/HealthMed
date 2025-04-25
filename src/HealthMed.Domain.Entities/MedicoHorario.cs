namespace HealthMed.Domain.Entities;

public class MedicoHorario : EntityBase
{
    public required DateTime DataHora { get; set; }
    public bool Ocupado { get; set; }

    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }
}
