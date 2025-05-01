namespace HealthMed.PacienteService.Application.Dtos;
public class PacienteLoginResponseDto
{
    public string? AccessToken { get; }

    public PacienteLoginResponseDto(string accessToken)
    {
        AccessToken = accessToken;
    }
}
