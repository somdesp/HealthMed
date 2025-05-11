using AutoMapper;
using HealthMed.BuildingBlocks.Authorization;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.PacienteService.Application.Contracts.Persistence;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Queries.BuscaPaciente;

public class BuscaPacientesQueryHandler : IRequestHandler<BuscaPacientesQuery, PacientesResponse>
{
    private readonly IAppUsuario _appUsuario;
    private readonly IMapper _mapper;
    private readonly IPacienteRepository _pacienteRepository;

    public BuscaPacientesQueryHandler(
        IAppUsuario appUsuario,
        IMapper mapper,
        IPacienteRepository pacienteRepository)
    {
        _appUsuario = appUsuario;
        _mapper = mapper;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<PacientesResponse> Handle(BuscaPacientesQuery request, CancellationToken cancellationToken)
    {
        var pacientes = _mapper.Map<IEnumerable<PacienteResponse>>(await
            _pacienteRepository.GetAsync(p => request.PacientesId.Contains(p.Id)));

        return new PacientesResponse(pacientes);
    }
}
