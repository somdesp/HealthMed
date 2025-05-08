using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Queries;
public class AgendaDisponivelQuery : IRequest<IEnumerable<AgendaDisponivelDto>>
{
    public int MedicoId { get; set; }
}
