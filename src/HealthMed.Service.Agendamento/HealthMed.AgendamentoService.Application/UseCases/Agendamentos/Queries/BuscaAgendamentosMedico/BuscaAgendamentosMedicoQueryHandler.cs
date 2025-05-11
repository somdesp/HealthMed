using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.Dtos;
using HealthMed.AgendamentoService.Domain.Entities.Enums;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Contracts.Requests;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosMedico;

public class BuscaAgendamentosMedicoQueryHandler : IRequestHandler<BuscaAgendamentosMedicoQuery, IEnumerable<MeusAgendamentosMedicoDto>>
{
    private readonly IAppUsuario _appUsuario;
    private readonly IMapper _mapper;
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IRequestClient<BuscaAgendasMedicoRequest> _requestClient;
    private readonly IRequestClient<PacientesRequest> _pacienteRequestClient;



    public BuscaAgendamentosMedicoQueryHandler(
        IAppUsuario appUsuario,
        IMapper mapper,
        IAgendamentoRepository agendamentoRepository,
        IRequestClient<BuscaAgendasMedicoRequest> requestClient,
        IRequestClient<PacientesRequest> pacienteRequestClient)
    {
        _appUsuario = appUsuario;
        _mapper = mapper;
        _agendamentoRepository = agendamentoRepository;
        _requestClient = requestClient;
        _pacienteRequestClient = pacienteRequestClient;
    }

    public async Task<IEnumerable<MeusAgendamentosMedicoDto>> Handle(BuscaAgendamentosMedicoQuery request, CancellationToken cancellationToken)
    {
        var response = await _requestClient.GetResponse<BuscaAgendasMedicoResponse>(new BuscaAgendasMedicoRequest(request.MedicoId));


        var agendamentos = await
            _agendamentoRepository.GetAsync(a => response.Message.Agendas.Select(a => a.Id).Contains(a.AgendaId)
                && (a.Status == AgendamentoStatus.Pendente || a.Status == AgendamentoStatus.Confirmado));

        var responseUser = await _pacienteRequestClient.GetResponse<PacientesResponse>(new PacientesRequest(agendamentos.Select(x => x.PacienteId)));

        var agendamentosDto = (from ag in agendamentos
                               join ru in responseUser.Message.PacienteResponses on ag.PacienteId equals ru.Id
                               join agM in response.Message.Agendas on ag.AgendaId equals agM.Id
                               select new MeusAgendamentosMedicoDto
                               {
                                   Id = ag.Id,
                                   PacienteId = ru.Id,
                                   Paciente = ru.Nome,
                                   DataHora = agM.DataHora,
                                   AgendaId = agM.Id,
                                   Status = ag.Status.ToString()

                               }).ToList();

        return agendamentosDto;
    }
}
