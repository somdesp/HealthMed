using AutoMapper;
using HealthMed.AgendamentoService.Application.Dtos;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.CancelaAgendamento;
using HealthMed.AgendamentoService.Application.UseCases.Agendamentos.Commands.NovoAgendamento;
using HealthMed.AgendamentoService.Domain.Entities;

namespace HealthMed.AgendamentoService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<NovoAgendamentoCommandRequest, Agendamento>();
        CreateMap<Agendamento, MeusAgendamentosMedicoDto>();
        CreateMap<CancelaAgendamentoCommandRequest, Agendamento>();
    }
}
