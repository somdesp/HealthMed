using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.PacienteService.Application.UseCases.Pacientes.Queries.BuscaPaciente;
using MassTransit;
using MediatR;

namespace HealthMed.PacienteService.Api.Consumers
{
    public class BuscaPacientesConsumer : IConsumer<PacientesRequest>
    {
        private readonly IMediator _mediator;

        public BuscaPacientesConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<PacientesRequest> context)
        {
            var result = await _mediator.Send(new BuscaPacientesQuery
            {
                PacientesId = context.Message.PacientesId
            });
            await context.RespondAsync(result);
        }
    }
}
