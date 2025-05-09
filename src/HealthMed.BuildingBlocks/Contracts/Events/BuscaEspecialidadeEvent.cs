using HealthMed.BuildingBlocks.Messaging;

namespace HealthMed.BuildingBlocks.Contracts.Events;

public record BuscaEspecialidadeEvent(string NomeEspecialidade) : IntegrationEvent;
