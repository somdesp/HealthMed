namespace HealthMed.AgendamentoService.Application.Dtos;

public class MeusAgendamentosResponseDto
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
}

public class MeusAgendamentosDto
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int AgendaId { get; set; }
    public string Medico { get; set; }
    public double ValorConsulta { get; set; }
    public required string Especialidade { get; set; }
}