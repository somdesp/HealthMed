namespace HealthMed.BuildingBlocks.Common;

public abstract class EntityBase
{
    public int Id { get; set; }
    public DateTime? DataCriacao { get; set; }

    protected EntityBase() => DataCriacao = DateTime.Now;
}
