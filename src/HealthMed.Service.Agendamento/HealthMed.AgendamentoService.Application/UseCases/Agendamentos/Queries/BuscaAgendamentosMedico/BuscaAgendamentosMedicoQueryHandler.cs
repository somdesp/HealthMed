using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.Dtos;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosMedico;

public class BuscaAgendamentosMedicoQueryHandler : IRequestHandler<BuscaAgendamentosMedicoQuery, IEnumerable<MeusAgendamentosResponseDto>>
{
    private readonly IAppUsuario _appUsuario;
    private readonly IMapper _mapper;
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IRequestClient<BuscaAgendasMedicoRequest> _requestClient;

    public BuscaAgendamentosMedicoQueryHandler(
        IAppUsuario appUsuario, 
        IMapper mapper, 
        IAgendamentoRepository agendamentoRepository,
        IRequestClient<BuscaAgendasMedicoRequest> requestClient)
    {
        _appUsuario = appUsuario;
        _mapper = mapper;
        _agendamentoRepository = agendamentoRepository;
        _requestClient = requestClient;
    }

    public async Task<IEnumerable<MeusAgendamentosResponseDto>> Handle(BuscaAgendamentosMedicoQuery request, CancellationToken cancellationToken)
    {
        var response = await _requestClient.GetResponse<BuscaAgendasMedicoResponse>(new BuscaAgendasMedicoRequest(request.MedicoId));

        var agendamentos = _mapper.Map<IEnumerable<MeusAgendamentosResponseDto>>(await
            _agendamentoRepository.GetAsync(a => response.Message.Agendas.Select(a => a.Id).Contains(a.AgendaId)));

        return agendamentos;
    }
}
