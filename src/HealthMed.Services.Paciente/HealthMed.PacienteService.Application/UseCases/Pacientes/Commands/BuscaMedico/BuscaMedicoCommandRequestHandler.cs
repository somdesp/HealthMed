using HealthMed.BuildingBlocks.Contracts.Events;
using HealthMed.BuildingBlocks.Contracts.Responses;
using MassTransit;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaMedico;
public class BuscaMedicoCommandRequestHandler : IRequestHandler<BuscaMedicoCommandRequest, IEnumerable<BuscaMedicoResponse>>
{
    private readonly IRequestClient<BuscaMedicoEvent> _requestClient;

    public BuscaMedicoCommandRequestHandler(IRequestClient<BuscaMedicoEvent> requestClient)
    {
        _requestClient = requestClient;

    }
    public async Task<IEnumerable<BuscaMedicoResponse>> Handle(BuscaMedicoCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await _requestClient.GetResponse<BuscaMedicosResponse>(
                    new BuscaMedicoEvent(
                        request.NomeMedico)
                    );

        return response.Message.MedicoResponse;
    }


}

