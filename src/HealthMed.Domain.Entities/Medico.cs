namespace HealthMed.Domain.Entities;

public class Medico : EntityBase
{
    public required string Nome { get; set; }
    public required string Crm { get; set; }
    public required string Senha { get; set; }
    public bool Ativo { get; set; }

    public int EspecialidadeId { get; set; }
    public MedicoEspecialidade? Especialidade { get; set; }

    public ICollection<Agendamento>? Agendamentos { get; set; }
}
