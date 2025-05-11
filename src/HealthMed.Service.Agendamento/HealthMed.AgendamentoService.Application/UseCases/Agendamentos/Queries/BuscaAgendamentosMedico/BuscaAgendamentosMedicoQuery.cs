using HealthMed.AgendamentoService.Application.Dtos;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosMedico;
public class BuscaAgendamentosMedicoQuery : IRequest<IEnumerable<MeusAgendamentosMedicoDto>>
{
    public int MedicoId { get; set; }
}
