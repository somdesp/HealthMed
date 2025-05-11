using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.Dtos;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosPaciente;

public class BuscaAgendamentosPacienteQueryHandler : IRequestHandler<BuscaAgendamentosPacienteQuery, IEnumerable<MeusAgendamentosPacienteDto>>
{
    private readonly IAppUsuario _appUsuario;
    private readonly IMapper _mapper;
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IRequestClient<BuscaMedicoPorAgendasRequest> _requestClient;

    public BuscaAgendamentosPacienteQueryHandler(
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

    public async Task<IEnumerable<MeusAgendamentosPacienteDto>> Handle(BuscaAgendamentosPacienteQuery request, CancellationToken cancellationToken)
    {
        var agendamentos = await
            _agendamentoRepository.GetAsync(a => a.PacienteId == _appUsuario.GetUsuarioId()
            && (a.Status == AgendamentoStatus.Confirmado || a.Status == AgendamentoStatus.Pendente));

        var response = await _requestClient.GetResponse<MeusAgendamentosResponse>(
            new BuscaMedicoPorAgendasRequest(agendamentos.Select(x => x.AgendaId)));

        var result = (from ma in response.Message.Agendamentos
                      join ag in agendamentos on ma.AgendaId equals ag.AgendaId
                      select new MeusAgendamentosPacienteDto
                      {
                          Id = ag.Id,
                          AgendaId = ma.AgendaId,
                          Medico = ma.Medico,
                          ValorConsulta = ma.ValorConsulta,
                          DataHora = ma.DataHora,
                          Especialidade = ma.Especialidade,
                          Status = ag.Status.ToString()
                      }).ToList();


        return result;
    }
}
