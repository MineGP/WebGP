using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebGP.Interfaces;

namespace WebGP.Models
{
    public class JwtConfig : IJwtConfig
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
        public TimeSpan Expires { get; set; }

        public SymmetricSecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}