namespace HealthMed.BuildingBlocks.Contracts.Responses;

public record AgendamentoResponse(int AgendaId, DateTime DataHora, string Medico, string Especialidade, double ValorConsulta);
