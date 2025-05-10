using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.AceitaRecusaAgendamento;

public class AceitaRecusaAgendamentoCommandRequestHandler : IRequestHandler<AceitaRecusaAgendamentoCommandRequest>
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public AceitaRecusaAgendamentoCommandRequestHandler(IAgendamentoRepository agendamentoRepository, IPublishEndpoint publishEndpoint)
    {
        _agendamentoRepository = agendamentoRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(AceitaRecusaAgendamentoCommandRequest request, CancellationToken cancellationToken)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(request.AgendamentoId);

        if (agendamento != null)
        {
            agendamento.Status = request.AceitaAgendamento ? AgendamentoStatus.Confirmado : AgendamentoStatus.Recusado;
            await _agendamentoRepository.UpdateAsync(agendamento);

            if (agendamento.Status == AgendamentoStatus.Recusado)
            {
                await _publishEndpoint.Publish(
                new AgendamentoRecusadoEvent(
                    agendamento.Id,
                    agendamento.AgendaId)
                );
            }
        }       
    }
}
