namespace HealthMed.Domain.Entities;

public class Paciente : EntityBase
{
    public required string Nome { get; set; }
    public required string Cpf { get; set; }
    public required string Senha { get; set; }
    public bool Ativo { get; set; }

    public ICollection<Agendamento>? Agendamentos { get; set; }
}
