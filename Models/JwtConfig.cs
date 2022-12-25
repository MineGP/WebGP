using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebGP.Models
{
    public class JwtConfig
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public string ISSUER { get; set; }
        public string AUDIENCE { get; set; }
        public string KEY { get; set; }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY ?? ""));
    }
}