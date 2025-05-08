using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using MediatR;
using MassTransit;
using HealthMed.BuildingBlocks.Messaging;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento;

public class NovoAgendamentoCommandRequestHandler : IRequestHandler<NovoAgendamentoCommandRequest>
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IMapper _mapper;
    private readonly IAppUsuario _appUsuario;
    private readonly IPublishEndpoint _publishEndpoint;

    public NovoAgendamentoCommandRequestHandler(
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

    public async Task Handle(NovoAgendamentoCommandRequest request, CancellationToken cancellationToken)
    {
        var novoAgendamento = _mapper.Map<Agendamento>(request);
        novoAgendamento.Status = AgendamentoStatus.Pendente;
        novoAgendamento.PacienteId = _appUsuario.GetUsuarioId();
        await _agendamentoRepository.AddAsync(novoAgendamento);

        await _publishEndpoint.Publish(
            new AgendamentoCriadoEvent(
                novoAgendamento.AgendaId,
                novoAgendamento.PacienteId)
            );
    }
}
