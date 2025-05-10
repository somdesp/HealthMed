using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.CancelaAgendamento;

public class CancelaAgendamentoCommandRequest : IRequest
{
    public int AgendamentoId { get; set; }
    public required string MotivoCancelamento { get; set; }
}
