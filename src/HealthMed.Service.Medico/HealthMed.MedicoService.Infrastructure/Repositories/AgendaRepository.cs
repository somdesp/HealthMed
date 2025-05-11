using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Domain.Entities;
using HealthMed.MedicoService.Infrastructure.Persistence;

namespace HealthMed.MedicoService.Infrastructure.Repositories
{
    public class AgendaRepository : Repository<AgendaMedico>, IAgendaRepository
    {
        public AgendaRepository(MedicoContext medicoContext) : base(medicoContext)
        {
        }
    }
}
