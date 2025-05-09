using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Infrastructure.Persistence;
using HealthMed.MedicoServiceService.Domain.Entities;

namespace HealthMed.MedicoService.Infrastructure.Repositories
{
    public class AgendaRepository : Repository<AgendaMedico>, IAgendaRepository
    {
        public AgendaRepository(MedicoContext medicoContext) : base(medicoContext)
        {
        }
    }
}
