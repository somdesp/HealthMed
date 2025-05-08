using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Queries;

public class AgendaDisponivelQueryHandler : IRequestHandler<AgendaDisponivelQuery, IEnumerable<AgendaDisponivelDto>>
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public AgendaDisponivelQueryHandler(IAgendaRepository agendaRepository, IMapper mapper)
    {
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<AgendaDisponivelDto>> Handle(AgendaDisponivelQuery request, CancellationToken cancellationToken)
    {
        var agenda = _mapper.Map<IEnumerable<AgendaDisponivelDto>>(
            await _agendaRepository.GetAsync(a => a.MedicoId == request.MedicoId)
            );

        return agenda;
    }
}
