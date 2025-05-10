namespace HealthMed.MedicoService.Application.Dtos
{
    public class MedicoDto
    {
        public required string Nome { get; set; }
        public required string Crm { get; set; }
        public EspecialidadeDto? Especialidade { get; set; }
    }
}
