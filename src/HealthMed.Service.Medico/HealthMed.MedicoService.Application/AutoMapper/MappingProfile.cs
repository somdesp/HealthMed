using AutoMapper;
using HealthMed.BuildingBlocks.Contracts;
using HealthMed.MedicoService.Domain.Entities;

namespace HealthMed.MedicoService.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MedicoResponse, Medico>().ReverseMap().ForMember(dest => dest.CRM,
               opts => opts.MapFrom(src => src.Crm)).ForMember(dest => dest.Especialidade,
               opts => opts.MapFrom(src => src.Especialidade.Nome)); ;
    }
}
