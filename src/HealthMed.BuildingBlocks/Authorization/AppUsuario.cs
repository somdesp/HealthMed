using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HealthMed.BuildingBlocks.Authorization;

public class AppUsuario : IAppUsuario
{
    private readonly IHttpContextAccessor _accessor;
    public string Nome => _accessor.HttpContext!.User.Identity!.Name!;

    public AppUsuario(IHttpContextAccessor accessor)
        => _accessor = accessor;

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext!.User.Claims;
    }

    public string GetUsuarioEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext!.User.GetUsuarioEmail()! : "";
    }

    public int GetUsuarioId()
    {
        return IsAuthenticated() ? Convert.ToInt32(_accessor.HttpContext!.User.GetUsuarioId()!) : 0;
    }

    public bool IsAuthenticated()
    {
        var isAuthenticated = _accessor?.HttpContext?.User?.Identity?.IsAuthenticated;
        return isAuthenticated ?? false;
    }
}
