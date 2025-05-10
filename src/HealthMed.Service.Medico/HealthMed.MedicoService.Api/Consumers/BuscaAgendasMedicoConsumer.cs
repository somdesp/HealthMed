using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.MedicoService.Application.UseCases.Agendas.Queries;
using MassTransit;
using MediatR;

namespace HealthMed.MedicoService.Api.Consumers;

public class BuscaAgendasMedicoConsumer : IConsumer<BuscaAgendasMedicoRequest>
{
    private readonly IMediator _mediator;

    public BuscaAgendasMedicoConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<BuscaAgendasMedicoRequest> context)
    {
        var agendas = await _mediator.Send(new MinhaAgendaQuery { MedicoId = context.Message.MedicoId });

        await context.RespondAsync(agendas);
    }
}
