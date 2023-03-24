using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using WebGP.Application.Common.Interfaces;

namespace WebGP;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public int? GetCurrentUserId()
    {
        var user = _contextAccessor.HttpContext?.User;
        var idClaim = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(idClaim?.Value)) return null;
        if (!int.TryParse(idClaim.Value, out var res)) return null;
        return res;
    }
}
