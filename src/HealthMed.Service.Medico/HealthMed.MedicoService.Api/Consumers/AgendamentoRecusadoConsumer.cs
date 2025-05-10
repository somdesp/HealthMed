using HealthMed.BuildingBlocks.Messaging;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.ReservaAgenda;
using MassTransit;
using MediatR;

namespace HealthMed.MedicoService.Api.Consumers;

public class AgendamentoRecusadoConsumer : IConsumer<AgendamentoRecusadoEvent>
{
    private readonly IMediator _mediator;

    public AgendamentoRecusadoConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AgendamentoRecusadoEvent> context)
    {
        await _mediator.Send(new ReservaAgendaCommandRequest
        {
            AgendaId = context.Message.AgendaId,
            ReservaAgenda = false
        });
    }
}
