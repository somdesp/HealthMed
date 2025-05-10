using HealthMed.BuildingBlocks.Messaging;

namespace HealthMed.BuildingBlocks.Contracts.Requests;

public record BuscaAgendasMedicoRequest(int MedicoId) : IntegrationEvent;
