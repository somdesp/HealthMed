namespace HealthMed.BuildingBlocks.Messaging;

public record BuscaMedicoPorNomeEvent(string Nome) : IntegrationEvent;
