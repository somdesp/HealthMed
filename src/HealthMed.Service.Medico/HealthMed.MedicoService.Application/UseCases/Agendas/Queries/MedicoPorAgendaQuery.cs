using HealthMed.BuildingBlocks.Contracts.Responses;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Queries
{
    public class MedicoPorAgendaQuery : IRequest<MeusAgendamentosResponse>
    {
        public required IEnumerable<int> AgendasId { get; set; }
    }

}
