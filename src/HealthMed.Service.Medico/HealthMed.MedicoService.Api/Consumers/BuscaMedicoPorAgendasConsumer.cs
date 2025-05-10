using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.MedicoService.Application.UseCases.Agendas.Queries;
using MassTransit;
using MediatR;

namespace HealthMed.MedicoService.Api.Consumers
{
    public class BuscaMedicoPorAgendasConsumer : IConsumer<BuscaMedicoPorAgendasRequest>
    {
        private readonly IMediator _mediator;

        public BuscaMedicoPorAgendasConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BuscaMedicoPorAgendasRequest> context)
        {
            var agendas = await _mediator.Send(new MedicoPorAgendaQuery { AgendasId = context.Message.AgendasId });

            await context.RespondAsync(agendas);
        }
    }
}
