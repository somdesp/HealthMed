using HealthMed.BuildingBlocks.Contracts.Responses;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaMeusAgendamentos;

public class BuscaMeusAgendamentosQuery : IRequest<IEnumerable<AgendamentoResponse>>
{
    public int PacienteId { get; set; }
}
