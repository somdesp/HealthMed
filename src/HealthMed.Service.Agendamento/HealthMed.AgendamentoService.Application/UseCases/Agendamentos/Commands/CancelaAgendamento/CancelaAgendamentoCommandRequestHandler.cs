using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.CancelaAgendamento;
public class CancelaAgendamentoCommandRequestHandler : IRequestHandler<CancelaAgendamentoCommandRequest>
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IMapper _mapper;
    private readonly IAppUsuario _appUsuario;
    private readonly IPublishEndpoint _publishEndpoint;

    public CancelaAgendamentoCommandRequestHandler(
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

    public async Task Handle(CancelaAgendamentoCommandRequest request, CancellationToken cancellationToken)
    {
        var agendamento = _mapper.Map<Agendamento>(request);
        agendamento.Status = AgendamentoStatus.Cancelado;

        await _agendamentoRepository.UpdateAsync(agendamento);

        await _publishEndpoint.Publish(
            new AgendamentoCanceladoEvent(
                agendamento.Id,
                agendamento.AgendaId)
            );
    }
}
