using HealthMed.PacienteService.Application.Contracts.Persistence;
using HealthMed.PacienteService.Application.Dtos;
using HealthMed.PacienteService.Application.Exceptions;
using HealthMed.PacienteService.Application.Settings;
using HealthMed.PacienteService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthMed.PacienteService.Application.UseCases.Pacientes.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, PacienteLoginResponseDto>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly TokenSettings _tokenSettings;

    public LoginQueryHandler(IPacienteRepository pacienteRepository, IOptions<TokenSettings> tokenSettings)
    {
        _pacienteRepository = pacienteRepository;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<PacienteLoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _pacienteRepository.FirstOrDefaultAsync(e => e.Ativo && e.Cpf!.ToString() == request.Cpf && e.Senha == request.Senha)
            ?? throw new ValidationException("Login", "CRM e/ou senha inválido(s)");

        var accessToken = await GenerateJwtToken(result);

        return new PacienteLoginResponseDto(accessToken);
    }

    private async Task<string> GenerateJwtToken(Paciente medico)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, medico.Id.ToString()),
                    new Claim(ClaimTypes.Name, medico.Nome),
                    new Claim(ClaimTypes.Role, "Medico")
                ]),
                Expires = DateTime.Now.AddHours(_tokenSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}