using HealthMed.BuildingBlocks.Messaging;

namespace HealthMed.BuildingBlocks.Contracts.Requests;

public record BuscaMedicoPorAgendasRequest(IEnumerable<int> AgendasId) : IntegrationEvent;

