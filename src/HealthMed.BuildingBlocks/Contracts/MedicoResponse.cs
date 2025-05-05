namespace HealthMed.BuildingBlocks.Contracts
{
    public record MedicoResponse(int Id, string Nome, string Especialidade, string CRM);

    public class BuscaMedicoCommand
    {
        public string Nome { get; }
        public string Especialidade { get; }

        public BuscaMedicoCommand(string nome, string especialidade)
        {
            Nome = nome;
            Especialidade = especialidade;
        }
    }

}
