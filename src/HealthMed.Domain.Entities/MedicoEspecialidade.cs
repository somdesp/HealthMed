namespace HealthMed.Domain.Entities;

public class MedicoEspecialidade : EntityBase
{
    public required string Nome { get; set; }

    public ICollection<Medico>? Medicos { get; set; }
}
