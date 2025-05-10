using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaEspecialidade;
public class BuscaMedicoPorEspecialidadeCommandRequestHandler : IRequestHandler<BuscaMedicoPorEspecialidadeCommandRequest, IEnumerable<BuscaMedicoResponse>>
{
    private readonly IRequestClient<BuscaMedicoPorEspecialidadeEvent> _requestClient;

    public BuscaMedicoPorEspecialidadeCommandRequestHandler(IRequestClient<BuscaMedicoPorEspecialidadeEvent> requestClient)
    {
        _requestClient = requestClient;

    }
    public async Task<IEnumerable<BuscaMedicoResponse>> Handle(BuscaMedicoPorEspecialidadeCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await _requestClient.GetResponse<BuscaMedicosResponse>(
                    new BuscaMedicoPorEspecialidadeEvent(
                        request.NomeEspecialidade)
                    );

        return response.Message.MedicoResponse;
    }


}

