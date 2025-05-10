using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.BuildingBlocks.Messaging;
using MassTransit;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaMedico;
public class BuscaMedicoPorNomeCommandRequestHandler : IRequestHandler<BuscaMedicoPorNomeCommandRequest, IEnumerable<BuscaMedicoResponse>>
{
    private readonly IRequestClient<BuscaMedicoPorNomeEvent> _requestClient;

    public BuscaMedicoPorNomeCommandRequestHandler(IRequestClient<BuscaMedicoPorNomeEvent> requestClient)
    {
        _requestClient = requestClient;

    }
    public async Task<IEnumerable<BuscaMedicoResponse>> Handle(BuscaMedicoPorNomeCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await _requestClient.GetResponse<BuscaMedicosResponse>(
                    new BuscaMedicoPorNomeEvent(
                        request.NomeMedico)
                    );

        return response.Message.MedicoResponse;
    }


}

