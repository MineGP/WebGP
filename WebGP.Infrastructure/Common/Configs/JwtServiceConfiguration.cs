using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebGP.Infrastructure.Common.Configs
{
    public class JwtServiceConfiguration
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;

        public SymmetricSecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
