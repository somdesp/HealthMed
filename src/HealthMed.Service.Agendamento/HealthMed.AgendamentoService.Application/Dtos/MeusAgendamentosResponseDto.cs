namespace HealthMed.AgendamentoService.Application.Dtos;

public class MeusAgendamentosResponseDto
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public required string Medico { get; set; }
}
