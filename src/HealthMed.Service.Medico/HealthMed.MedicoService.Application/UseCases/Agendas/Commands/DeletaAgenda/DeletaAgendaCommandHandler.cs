using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoServiceService.Domain.Entities;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.DeletaAgenda;

public class DeletaAgendaCommandHandler : IRequestHandler<DeletaAgendaCommandRequest>
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public DeletaAgendaCommandHandler(IAgendaRepository agendaRepository, IMapper mapper)
    {
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public async Task Handle(DeletaAgendaCommandRequest request, CancellationToken cancellationToken)
    {
        var agenda = _mapper.Map<AgendaMedico>(request);
        await _agendaRepository.UpdateAsync(agenda);
    }
}