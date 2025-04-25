namespace HealthMed.BuildingBlocks.Messaging;

public abstract record IntegrationEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime DataCriacao { get; init; } = DateTime.Now;
}
