using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.Dtos;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaMeusAgendamentos;

public class BuscaMeusAgendamentosQueryHandler : IRequestHandler<BuscaMeusAgendamentosQuery, IEnumerable<MeusAgendamentosDto>>
{
    private readonly IAppUsuario _appUsuario;
    private readonly IMapper _mapper;
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IRequestClient<BuscaMedicoPorAgendasRequest> _requestClient;

    public BuscaMeusAgendamentosQueryHandler(
        IAppUsuario appUsuario,
        IMapper mapper,
        IAgendamentoRepository agendamentoRepository,
        IRequestClient<BuscaMedicoPorAgendasRequest> requestClient
        )
    {
        _appUsuario = appUsuario;
        _mapper = mapper;
        _agendamentoRepository = agendamentoRepository;
        _requestClient = requestClient;
    }

    public async Task<IEnumerable<MeusAgendamentosDto>> Handle(BuscaMeusAgendamentosQuery request, CancellationToken cancellationToken)
    {
        var agendamentos = await
            _agendamentoRepository.GetAsync(a => a.PacienteId == _appUsuario.GetUsuarioId());

        var response = await _requestClient.GetResponse<MeusAgendamentosResponse>(
            new BuscaMedicoPorAgendasRequest(agendamentos.Select(x => x.AgendaId)));

        var result = (from ma in response.Message.Agendamentos
                      join ag in agendamentos on ma.AgendaId equals ag.AgendaId
                      select new MeusAgendamentosDto
                      {
                          Id = ag.Id,
                          AgendaId = ma.AgendaId,
                          Medico = ma.Medico,
                          ValorConsulta = ma.ValorConsulta,
                          DataHora = ma.DataHora,
                          Especialidade = ma.Especialidade
                      }).ToList();


        return result;
    }
}
