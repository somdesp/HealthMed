using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Domain.Entities;
using HealthMed.MedicoService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.MedicoService.Infrastructure.Repositories;

public class MedicoRepository : Repository<Medico>, IMedicoRepository
{
    private readonly MedicoContext _medicoContexto;
    public MedicoRepository(MedicoContext medicoContext) : base(medicoContext) { _medicoContexto = medicoContext; }

    public async Task<IEnumerable<Medico>> BuscaEspecialidade(string nome)
    {
        var especialidades = await _medicoContexto.Especialidades.Where(x => x.Nome.Contains(nome)).ToListAsync();
        var especialidadeId = especialidades.Select(x => x.Id).ToArray();

        var result = await _medicoContexto.Medicos.Include(x => x.Especialidade).Where(x => especialidadeId.Contains(x.EspecialidadeId)).ToListAsync();

        return result;
    }

    public async Task<IEnumerable<Medico>> BuscaMedico(string nome)
    {
        var result = await _medicoContexto.Medicos.Include(x => x.Especialidade).Where(x => x.Nome.Contains(nome)).ToListAsync();
        return result;

    }

    public async Task<IEnumerable<Medico>> BuscaMedicoPorId(IEnumerable<int> medicoId)
    {
        var result = await _medicoContexto.Medicos.Include(x => x.Especialidade).Where(x => medicoId.Contains(x.Id)).ToListAsync();
        return result;
    }

}
