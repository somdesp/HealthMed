namespace HealthMed.BuildingBlocks.Contracts.Responses;

public class BuscaAgendasMedicoResponse
{
    public required IEnumerable<AgendaResponse> Agendas { get; set; }
}

