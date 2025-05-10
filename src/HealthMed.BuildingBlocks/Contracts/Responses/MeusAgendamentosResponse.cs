namespace HealthMed.BuildingBlocks.Contracts.Responses;

public record class MeusAgendamentosResponse
{
    public IEnumerable<AgendamentoResponse> Agendamentos { get; set; } = Enumerable.Empty<AgendamentoResponse>();
    public int Total { get; set; }
}
