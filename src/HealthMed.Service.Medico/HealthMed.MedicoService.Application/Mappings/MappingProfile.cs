using AutoMapper;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.DeletaAgenda;
using HealthMed.MedicoService.Application.UseCases.Agendas.Commands.NovaAgenda;
using HealthMed.MedicoService.Domain.Entities;

namespace HealthMed.MedicoService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NovaAgendaCommandRequest, AgendaMedico>();
        CreateMap<DeletaAgendaCommandRequest, AgendaMedico>();

        CreateMap<Especialidade, EspecialidadeDto>();
        CreateMap<Medico, MedicoDto>();

        CreateMap<AgendaMedico, AgendaDisponivelDto>();

        CreateMap<AgendaDisponivelDto, AgendaResponse>();

    }
}
