using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.DeletaAgendamento;

public class DeletaAgendamentoCommandRequest : IRequest
{
    public int AgendaId { get; set; }
}
