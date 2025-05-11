using HealthMed.BuildingBlocks.Contracts.Responses;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Agendas.Queries;

public class BuscaAgendaReservadaMedicoQuery : IRequest<BuscaAgendasMedicoResponse>
{
    public int MedicoId { get; set; }
}
