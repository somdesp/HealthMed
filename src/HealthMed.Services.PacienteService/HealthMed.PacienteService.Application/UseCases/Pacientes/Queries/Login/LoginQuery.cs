using HealthMed.PacienteService.Application.Dtos;
using MediatR;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Queries.Login;

public class LoginQuery : IRequest<PacienteLoginResponseDto>
{
    public required string Cpf { get; set; }
    public required string Senha { get; set; }
}
