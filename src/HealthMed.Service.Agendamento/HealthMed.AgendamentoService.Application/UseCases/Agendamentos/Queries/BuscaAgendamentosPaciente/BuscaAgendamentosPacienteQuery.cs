using HealthMed.AgendamentoService.Application.Dtos;
using MediatR;

namespace HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Queries.BuscaAgendamentosPaciente;

public class BuscaAgendamentosPacienteQuery : IRequest<IEnumerable<MeusAgendamentosPacienteDto>>
{
    public int PacienteId { get; set; }
}
