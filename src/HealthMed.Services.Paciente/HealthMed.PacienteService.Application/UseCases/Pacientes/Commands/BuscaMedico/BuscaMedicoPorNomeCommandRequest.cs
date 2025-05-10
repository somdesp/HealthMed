using HealthMed.BuildingBlocks.Contracts.Responses;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Commands.BuscaMedico
{
    public class BuscaMedicoPorNomeCommandRequest : IRequest<IEnumerable<BuscaMedicoResponse>>
    {
        public required string NomeMedico { get; set; }
    }
}
