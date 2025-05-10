namespace HealthMed.MedicoService.Application.Dtos;

public class AgendaDisponivelDto
{
    public int Id { get; set; }
    public int MedicoId { get; set; }
    public DateTime DataHora { get; set; }
    public required string Medico { get; set; }
    public double ValorConsulta { get; set; }
}
