using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoServiceService.Domain.Entities;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;

public class NovaAgendaCommandRequestHandler : IRequestHandler<NovaAgendaCommandRequest, bool>
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public NovaAgendaCommandRequestHandler(IAgendaRepository agendaRepository, IMapper mapper)
    {
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(NovaAgendaCommandRequest request, CancellationToken cancellationToken)
    {
        var novaAgenda = _mapper.Map<AgendaMedico>(request);
        var result = await _agendaRepository.AddAsync(novaAgenda);

        return result != null;
    }
}
