using AutoMapper;
using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Application.Dtos;
using HealthMed.BuildingBlocks.Authorization;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaMeusAgendamentos;

public class BuscaMeusAgendamentosQueryHandler : IRequestHandler<BuscaMeusAgendamentosQuery, IEnumerable<MeusAgendamentosResponseDto>>
{
    private readonly IAppUsuario _appUsuario;
    private readonly IMapper _mapper;
    private readonly IAgendamentoRepository _agendamentoRepository;

    public BuscaMeusAgendamentosQueryHandler(IAppUsuario appUsuario, IMapper mapper, IAgendamentoRepository agendamentoRepository)
    {
        _appUsuario = appUsuario;
        _mapper = mapper;
        _agendamentoRepository = agendamentoRepository;
    }

    public async Task<IEnumerable<MeusAgendamentosResponseDto>> Handle(BuscaMeusAgendamentosQuery request, CancellationToken cancellationToken)
    {
        var agendamentos = _mapper.Map<IEnumerable<MeusAgendamentosResponseDto>>(await 
            _agendamentoRepository.GetAsync(a => a.PacienteId == _appUsuario.GetUsuarioId()));

        return agendamentos;
    }
}
