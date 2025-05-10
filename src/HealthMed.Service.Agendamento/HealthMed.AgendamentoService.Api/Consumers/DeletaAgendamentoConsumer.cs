using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.DeletaAgendamento;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Api.Consumers
{
    public class DeletaAgendamentoConsumer : IConsumer<DeletaAgendamentoEvent>
    {
        private readonly IMediator _mediator;

        public DeletaAgendamentoConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<DeletaAgendamentoEvent> context)
        {
            await _mediator.Send(new DeletaAgendamentoCommandRequest
            {
                AgendaId = context.Message.AgendaId
            });
        }
    }
}
