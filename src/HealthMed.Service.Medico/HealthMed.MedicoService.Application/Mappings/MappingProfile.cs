using AutoMapper;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.AlteraAgenda;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;
using HealthMed.MedicoService.Domain.Entities;
using HealthMed.MedicoServiceService.Domain.Entities;

namespace HealthMed.MedicoService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NovaAgendaCommandRequest, AgendaMedico>();
        CreateMap<AlteraAgendaCommandRequest, AgendaMedico>();

        CreateMap<Especialidade, EspecilidadeDto>();
        CreateMap<Medico, MedicoDto>();
    }
}
