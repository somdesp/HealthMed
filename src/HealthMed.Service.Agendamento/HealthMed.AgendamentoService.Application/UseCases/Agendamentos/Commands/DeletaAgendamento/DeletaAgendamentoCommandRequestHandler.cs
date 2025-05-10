using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.DeletaAgendamento;
public class DeletaAgendamentoCommandRequestHandler : IRequestHandler<DeletaAgendamentoCommandRequest>
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IMapper _mapper;
    private readonly IAppUsuario _appUsuario;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeletaAgendamentoCommandRequestHandler(
        IAgendamentoRepository agendamentoRepository,
        IMapper mapper,
        IAppUsuario appUsuario,
        IPublishEndpoint publishEndpoint)
    {
        _agendamentoRepository = agendamentoRepository;
        _mapper = mapper;
        _appUsuario = appUsuario;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(DeletaAgendamentoCommandRequest request, CancellationToken cancellationToken)
    {
        var agendamento = await _agendamentoRepository.FirstOrDefaultAsync(a => a.AgendaId == request.AgendaId);
        if (agendamento != null)
        {
            agendamento.Status = AgendamentoStatus.Recusado;
            agendamento.MotivoCancelamento = "Agenda cancelada pelo médico.";

            await _agendamentoRepository.UpdateAsync(agendamento);
        }
    }
}
