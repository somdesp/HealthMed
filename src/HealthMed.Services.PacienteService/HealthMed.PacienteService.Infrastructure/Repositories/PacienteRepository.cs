using HealthMed.PacienteService.Application.Contracts.Persistence;
using HealthMed.PacienteService.Domain.Entities;
using HealthMed.PacienteService.Infrastructure.Persistence;

namespace HealthMed.PacienteService.Infrastructure.Repositories;

public class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(PacienteContext medicoContext) : base(medicoContext) { }
}
