using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Commands.DeletaAgenda;

public class DeletaAgendaCommandRequest : IRequest
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int MedicoId { get; set; }
}
