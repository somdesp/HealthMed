using HealthMed.MedicoService.Application.Dtos;
using MediatR;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Queries.Login;

public class LoginQuery : IRequest<MedicoLoginResponseDto>
{
    public required string Crm { get; set; }
    public required string Senha { get; set; }
}
