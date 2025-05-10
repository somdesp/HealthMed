using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaEspecialidade
{
    public class BuscaMedicoPorEspecialidadeCommandRequest : IRequest<IEnumerable<MedicoDto>>
    {
        public required string NomeEspecialidade { get; set; }
    }
}
