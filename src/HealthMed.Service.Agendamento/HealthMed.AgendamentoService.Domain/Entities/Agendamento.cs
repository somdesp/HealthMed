using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Common;

namespace HealthMed.AgendamentoService.Domain.Entities;

public class Agendamento : EntityBase
{
    public int PacienteId { get; set; }
    public AgendamentoStatus Status { get; set; } = AgendamentoStatus.Pendente;
    public string? JustificativaCancelamento { get; set; }
    public int AgendaId { get; set; }
}
