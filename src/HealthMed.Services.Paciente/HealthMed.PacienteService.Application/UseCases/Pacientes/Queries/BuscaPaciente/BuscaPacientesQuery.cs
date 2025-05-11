using HealthMed.BuildingBlocks.Contracts.Responses;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Queries.BuscaPaciente;
public class BuscaPacientesQuery : IRequest<PacientesResponse>
{
    public required IEnumerable<int> PacientesId { get; set; }
}
