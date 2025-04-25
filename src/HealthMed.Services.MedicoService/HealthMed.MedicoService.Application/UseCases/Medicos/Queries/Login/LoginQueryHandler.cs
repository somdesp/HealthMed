using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.Exceptions;
using HealthMed.MedicoService.Application.Settings;
using HealthMed.MedicoService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthMed.MedicoService.Application.UseCases.Medicos.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, MedicoLoginResponseDto>
{
    private readonly IMedicoRepository _medicoRepository;
    private readonly TokenSettings _tokenSettings;

    public LoginQueryHandler(IMedicoRepository medicoRepository, IOptions<TokenSettings> tokenSettings)
    {
        _medicoRepository = medicoRepository;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<MedicoLoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _medicoRepository.FirstOrDefaultAsync(e => e.Ativo && e.Crm == request.Crm && e.Senha == request.Senha) 
            ?? throw new ValidationException("Login", "CRM e/ou senha inválido(s)");

        var accessToken = await GenerateJwtToken(result);

        return new MedicoLoginResponseDto(accessToken);
    }

    private async Task<string> GenerateJwtToken(Medico medico)
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
