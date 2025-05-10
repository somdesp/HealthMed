namespace HealthMed.BuildingBlocks.Messaging;

public record BuscaMedicoPorEspecialidadeEvent(string NomeEspecialidade) : IntegrationEvent;
