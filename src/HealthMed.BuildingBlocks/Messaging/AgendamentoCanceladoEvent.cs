namespace HealthMed.BuildingBlocks.Messaging;

public record AgendamentoCanceladoEvent(int AgendamentoId, int AgendaId) : IntegrationEvent;
