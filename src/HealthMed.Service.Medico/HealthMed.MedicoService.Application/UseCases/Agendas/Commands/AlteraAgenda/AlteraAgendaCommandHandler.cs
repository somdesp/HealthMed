using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoServiceService.Domain.Entities;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;

public class AlteraAgendaCommandHandler : IRequestHandler<AlteraAgendaCommandRequest>
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public AlteraAgendaCommandHandler(IAgendaRepository agendaRepository, IMapper mapper)
    {
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public async Task Handle(AlteraAgendaCommandRequest request, CancellationToken cancellationToken)
    {
        //TODO: implementar o metodo
        var agenda = _mapper.Map<AgendaMedico>(request);
        await _agendaRepository.UpdateAsync(agenda);
    }
}