using HealthMed.BuildingBlocks.Contracts.Responses;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaEspecialidade
{
    public class BuscaMedicoPorEspecialidadeCommandRequest : IRequest<IEnumerable<BuscaMedicoResponse>>
    {
        public required string NomeEspecialidade { get; set; }
    }
}
