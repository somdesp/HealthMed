using HealthMed.BuildingBlocks.Messaging;

namespace HealthMed.BuildingBlocks.Contracts.Events;

public record BuscaMedicoEvent(string Nome) : IntegrationEvent;
