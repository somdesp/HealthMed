using AutoMapper;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Queries;

public class BuscaAgendaReservadaMedicoQueryHandler : IRequestHandler<BuscaAgendaReservadaMedicoQuery, BuscaAgendasMedicoResponse>
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IMapper _mapper;

    public BuscaAgendaReservadaMedicoQueryHandler(IAgendaRepository agendaRepository, IMapper mapper)
    {
        _agendaRepository = agendaRepository;
        _mapper = mapper;
    }

    public async Task<BuscaAgendasMedicoResponse> Handle(BuscaAgendaReservadaMedicoQuery request, CancellationToken cancellationToken)
    {
        var agendas = _mapper.Map<IEnumerable<AgendaDisponivelDto>>(
            await _agendaRepository.GetAsync(a => a.MedicoId == request.MedicoId && a.Reservada)
            );

        var response = new BuscaAgendasMedicoResponse
        {
            Agendas = _mapper.Map<IEnumerable<AgendaResponse>>(agendas)
        };

        return response;
    }
}
