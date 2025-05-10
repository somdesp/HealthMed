using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Commands.BuscaMedico
{
    public class BuscaMedicoPorNomeCommantRequest : IRequest<IEnumerable<MedicoDto>>
    {
        public required string NomeMedico { get; set; }
    }
}
