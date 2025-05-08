using HealthMed.BuildingBlocks.Messaging;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.ReservaAgenda;
using MassTransit;
using MediatR;

namespace HealthMed.MedicoService.Api.Consumers;

public class AgendamentoCriadoConsumer : IConsumer<AgendamentoCriadoEvent>
{
    private readonly IMediator _mediator;

    public AgendamentoCriadoConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AgendamentoCriadoEvent> context)
    {
        await _mediator.Send(new ReservaAgendaCommandRequest
        {
            AgendaId = context.Message.AgendaId
        });
    }
}
