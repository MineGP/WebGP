using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebGP.Interfaces.Config;

namespace WebGP.Models.Config;

public class JwtConfig : IJwtConfig
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Key { get; set; } = null!;

    public SymmetricSecurityKey GetSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}