using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;

public class AlteraAgendaCommandRequest : IRequest
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int MedicoId { get; set; }
}
