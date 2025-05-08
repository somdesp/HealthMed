namespace HealthMed.BuildingBlocks.Contracts
{
    public record MedicoResponse(int Id, string Nome, string Especialidade, string CRM);

    public record MedicosResponse(IEnumerable<MedicoResponse> MedicoResponse);

    public class BuscaMedicoCommand
    {
        public string Nome { get; }

        public BuscaMedicoCommand(string nome)
        {
            Nome = nome;
        }
    }

    public class BuscaMedicoEspecialidadeCommand
    {
        public string Nome { get; }

        public BuscaMedicoEspecialidadeCommand(string nome)
        {
            Nome = nome;
        }
    }

}
