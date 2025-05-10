using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento;

public class NovoAgendamentoCommandRequestHandler : IRequestHandler<NovoAgendamentoCommandRequest, bool>
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

    public async Task<bool> Handle(NovoAgendamentoCommandRequest request, CancellationToken cancellationToken)
    {
        var novoAgendamento = _mapper.Map<Agendamento>(request);
        novoAgendamento.Status = AgendamentoStatus.Pendente;
        novoAgendamento.PacienteId = _appUsuario.GetUsuarioId();
        await _agendamentoRepository.AddAsync(novoAgendamento);

        await _publishEndpoint.Publish(
            new AgendamentoCriadoEvent(
                novoAgendamento.PacienteId, novoAgendamento.AgendaId)
            );

        return true;
    }
}
