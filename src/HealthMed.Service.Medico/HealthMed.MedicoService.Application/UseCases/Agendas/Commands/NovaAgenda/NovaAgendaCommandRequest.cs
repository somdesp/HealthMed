using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;
public class NovaAgendaCommandRequest : IRequest<bool>
{
    public DateTime DataHora { get; set; }
}
