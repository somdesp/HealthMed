using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.AceitaRecusaAgendamento;

public class AceitaRecusaAgendamentoCommandRequest : IRequest
{
    public int AgendamentoId { get; set; }
    public bool AceitaAgendamento { get; set; }
}
