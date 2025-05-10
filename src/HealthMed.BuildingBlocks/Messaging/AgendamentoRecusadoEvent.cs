namespace HealthMed.BuildingBlocks.Messaging;

public record AgendamentoRecusadoEvent(
    int AgendamentoId,
    int AgendaId) : IntegrationEvent;
