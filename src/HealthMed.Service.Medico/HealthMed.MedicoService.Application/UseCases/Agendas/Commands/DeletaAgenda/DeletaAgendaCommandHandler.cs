using AutoMapper;
using HealthMed.BuildingBlocks.Messaging;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Domain.Entities;
using HealthMed.MedicoServiceService.Domain.Entities;
using MassTransit;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.DeletaAgenda;

public class DeletaAgendaCommandHandler : IRequestHandler<DeletaAgendaCommandRequest>
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeletaAgendaCommandHandler(
        IAgendaRepository agendaRepository,
        IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _agendaRepository = agendaRepository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(DeletaAgendaCommandRequest request, CancellationToken cancellationToken)
    {
        var agenda = await _agendaRepository.FirstOrDefaultAsync(x => x.Id == request.Id && x.MedicoId == request.MedicoId && !x.Reservada);
        if (agenda != null)
            await _agendaRepository.DeleteAsync(agenda);

    }
}