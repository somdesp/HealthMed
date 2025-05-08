using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;

public class AlteraAgendaCommandRequest : IRequest
{
    public int AgendaId { get; set; }
    public DateTime DataHora { get; set; }
}
