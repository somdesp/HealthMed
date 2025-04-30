using HealthMed.BuildingBlocks.Configurations;
using HealthMed.PacienteService.Application.Contracts.Persistence;
using HealthMed.PacienteService.Application.Dtos;
using HealthMed.PacienteService.Application.Exceptions;
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
    const string ChaveSenha = "HealthMed#2025";

    public LoginQueryHandler(IPacienteRepository pacienteRepository, IOptions<TokenSettings> tokenSettings)
    {
        _pacienteRepository = pacienteRepository;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<PacienteLoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        string senhaCriptografada = CryptoHelper.CriptografarSenha(request.Senha, ChaveSenha);
        var result = await _pacienteRepository.FirstOrDefaultAsync(e => e.Ativo && e.Cpf.Numero == request.Cpf && e.Senha == senhaCriptografada)
            ?? throw new ValidationException("Login", "CPF e/ou senha inválido(s)");

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
               [
                   new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.Nome),
                    new Claim("doc", result.Cpf.ToString()),
                    new Claim(ClaimTypes.Role, "Paciente")
               ]);


        var accessToken = await GenerateJwtToken(claimsIdentity);

        return new PacienteLoginResponseDto(accessToken);
    }

    private async Task<string> GenerateJwtToken(ClaimsIdentity claimsIdentity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddHours(_tokenSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}