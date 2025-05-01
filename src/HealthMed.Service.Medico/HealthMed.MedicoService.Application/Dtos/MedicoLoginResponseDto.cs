namespace HealthMed.MedicoService.Application.Dtos;

public class MedicoLoginResponseDto
{
    public string? AccessToken { get; }

    public MedicoLoginResponseDto(string accessToken)
    {
        AccessToken = accessToken;
    }
}
