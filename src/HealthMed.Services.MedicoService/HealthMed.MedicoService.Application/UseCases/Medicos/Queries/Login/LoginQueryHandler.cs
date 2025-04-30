using HealthMed.BuildingBlocks.Configurations;
using HealthMed.MedicoService.Application.Contracts.Persistence;
using HealthMed.MedicoService.Application.Dtos;
using HealthMed.MedicoService.Application.Exceptions;
using HealthMed.MedicoService.Application.Settings;
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
    const string ChaveSenha = "HealthMed#2025";

    public LoginQueryHandler(IMedicoRepository medicoRepository, IOptions<TokenSettings> tokenSettings)
    {
        _medicoRepository = medicoRepository;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<MedicoLoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        string senhaCriptografada = CryptoHelper.CriptografarSenha(request.Senha, ChaveSenha);
        var result = await _medicoRepository.FirstOrDefaultAsync(e => e.Ativo && e.Crm == request.Crm && e.Senha == senhaCriptografada)
            ?? throw new ValidationException("Login", "CRM e/ou senha inválido(s)");

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
               [
                   new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.Nome),
                    new Claim(ClaimTypes.Role, "Medico")
               ]);

        var accessToken = await GenerateJwtToken(claimsIdentity);

        return new MedicoLoginResponseDto(accessToken);
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
