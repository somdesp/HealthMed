using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Domain.Entities;
using HealthMed.MedicoService.Infrastructure.Persistence;

namespace HealthMed.MedicoService.Infrastructure.Repositories;

public class MedicoRepository : Repository<Medico>, IMedicoRepository
{
    public MedicoRepository(MedicoContext medicoContext) : base(medicoContext) { }
}
