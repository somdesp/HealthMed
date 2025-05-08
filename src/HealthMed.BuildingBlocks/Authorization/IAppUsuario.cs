using System.Security.Claims;

namespace HealthMed.BuildingBlocks.Authorization;

public interface IAppUsuario
{
    string Nome { get; }
    int GetUsuarioId();
    string GetUsuarioEmail();
    bool IsAuthenticated();
    IEnumerable<Claim> GetClaimsIdentity();
}
