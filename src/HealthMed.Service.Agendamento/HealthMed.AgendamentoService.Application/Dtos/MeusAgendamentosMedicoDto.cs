namespace HealthMed.AgendamentoService.Application.Dtos;

public class MeusAgendamentosMedicoDto
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int PacienteId { get; set; }
    public string? Status { get; set; }
    public int AgendaId { get; set; }
    public string? Paciente { get; set; }
}

public class MeusAgendamentosPacienteDto
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int AgendaId { get; set; }
    public string? Medico { get; set; }
    public double ValorConsulta { get; set; }
    public required string Especialidade { get; set; }
    public string? Status { get; set; }
}