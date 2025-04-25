namespace HealthMed.PacienteService.Application.Settings;

public class TokenSettings
{
    public string Secret { get; set; } = string.Empty;
    public int ExpiracaoHoras { get; set; }
}
