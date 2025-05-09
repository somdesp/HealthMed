using HealthMed.BuildingBlocks.Contracts.Events;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaEspecialidade;
public class BuscaEspecialidadeCommandRequestHandler : IRequestHandler<BuscaEspecialidadeCommandRequest, IEnumerable<BuscaMedicoResponse>>
{
    private readonly IRequestClient<BuscaEspecialidadeEvent> _requestClient;

    public BuscaEspecialidadeCommandRequestHandler(IRequestClient<BuscaEspecialidadeEvent> requestClient)
    {
        _requestClient = requestClient;

    }
    public async Task<IEnumerable<BuscaMedicoResponse>> Handle(BuscaEspecialidadeCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await _requestClient.GetResponse<BuscaMedicosResponse>(
                    new BuscaEspecialidadeEvent(
                        request.NomeEspecialidade)
                    );

        return response.Message.MedicoResponse;
    }


}

