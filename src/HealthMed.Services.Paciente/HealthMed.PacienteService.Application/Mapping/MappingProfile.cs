using AutoMapper;
using HealthMed.BuildingBlocks.Contracts.Responses;
using HealthMed.PacienteService.Domain.Entities;

namespace HealthMed.PacienteService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Paciente, PacienteResponse>();
    }
}
