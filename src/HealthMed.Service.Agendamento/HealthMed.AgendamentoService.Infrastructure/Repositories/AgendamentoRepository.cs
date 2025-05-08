using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Domain.Entities;
using HealthMed.AgendamentoService.Infrastructure.Persistence;

namespace HealthMed.AgendamentoService.Infrastructure.Repositories;

public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
{
    private readonly AgendamentoContext _AgendamentoContexto;
    public AgendamentoRepository(AgendamentoContext AgendamentoContext) 
        : base(AgendamentoContext) 
    { 
        _AgendamentoContexto = AgendamentoContext; 
    }
}
