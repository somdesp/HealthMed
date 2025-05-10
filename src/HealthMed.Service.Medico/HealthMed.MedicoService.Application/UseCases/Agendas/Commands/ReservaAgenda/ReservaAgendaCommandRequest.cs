using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.ReservaAgenda;
public class ReservaAgendaCommandRequest : IRequest
{
    public int AgendaId { get; set; }
    public bool ReservaAgenda { get; set; }
}
