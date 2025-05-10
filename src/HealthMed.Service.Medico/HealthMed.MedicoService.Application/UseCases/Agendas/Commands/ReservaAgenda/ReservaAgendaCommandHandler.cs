using AutoMapper;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.ReservaAgenda;

public class ReservaAgendaCommandHandler : IRequestHandler<ReservaAgendaCommandRequest>
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public ReservaAgendaCommandHandler(IAgendaRepository agendaRepository, IMapper mapper)
    {
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public async Task Handle(ReservaAgendaCommandRequest request, CancellationToken cancellationToken)
    {
        var agenda = await _agendaRepository.GetByIdAsync(request.AgendaId);

        if (agenda != null)
        {
            agenda.Reservada = request.ReservaAgenda;
            await _agendaRepository.UpdateAsync(agenda);
        }
    }
}
