using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento;

public class NovoAgendamentoCommandRequest : IRequest
{
    public int AgendaId { get; set; }
}
