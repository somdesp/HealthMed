using HealthMed.AgendamentoService.Application.Dtos;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaMeusAgendamentos;

public class BuscaMeusAgendamentosQuery : IRequest<IEnumerable<MeusAgendamentosDto>>
{
    public int PacienteId { get; set; }
}
