using HealthMed.MedicoService.Domain.Entities;

namespace HealthMed.MedicoService.Application.Contracts.Persistence;

public interface IMedicoRepository : IRepository<Medico>
{
    Task<IEnumerable<Medico>> BuscaEspecialidade(string nome);
    Task<IEnumerable<Medico>> BuscaMedico(string nome);
    Task<IEnumerable<Medico>> BuscaMedicoPorId(IEnumerable<int> medicoId);
}
