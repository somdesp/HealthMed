using HealthMed.Domain.Entities.Enums;

namespace HealthMed.Domain.Entities;

public class Agendamento : EntityBase
{
    public DateTime DataHora { get; set; }
    public AgendamentoStatus Status { get; set; } = AgendamentoStatus.Pendente;

    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }

    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
}
