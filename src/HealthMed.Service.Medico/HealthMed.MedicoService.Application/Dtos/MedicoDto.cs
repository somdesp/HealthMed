namespace HealthMed.MedicoService.Application.Dtos
{
    public class MedicoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Crm { get; set; }

        public EspecilidadeDto? Especilidade { get; set; }
    }
}
