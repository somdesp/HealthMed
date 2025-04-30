namespace HealthMed.BuildingBlocks.Configurations;

public class TokenSettings
{
    public string Secret { get; set; } = string.Empty;
    public int ExpiracaoHoras { get; set; }
}
