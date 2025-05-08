namespace HealthMed.BuildingBlocks.Messaging;

public record AgendamentoCriadoEvent(int PacienteId, int AgendaId) : IntegrationEvent;
