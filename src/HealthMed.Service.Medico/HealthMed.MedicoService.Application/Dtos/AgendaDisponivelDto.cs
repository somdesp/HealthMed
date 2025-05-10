namespace HealthMed.MedicoService.Application.Dtos;

public class AgendaDisponivelDto
{
    public int Id { get; set; }
    public int MedicoId { get; set; }
    public DateTime DataHora { get; set; }
}
