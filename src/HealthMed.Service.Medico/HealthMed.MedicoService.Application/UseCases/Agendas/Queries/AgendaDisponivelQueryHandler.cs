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
        var agendaDisponivel =
            await _agendaRepository.GetAsync(a => a.MedicoId == request.MedicoId && !a.Reservada, null, "Medico");


        var agenda = (from ad in agendaDisponivel
                      select new AgendaDisponivelDto
                      {
                          DataHora = ad.DataHora,
                          Id = ad.Id,
                          MedicoId = ad.MedicoId,
                          Medico = ad.Medico.Nome,
                          ValorConsulta = ad.Medico.ValorConsulta
                      }).ToList();

        return agenda;
    }
}
