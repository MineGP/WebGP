using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.Common.Configs;

namespace WebGP.Infrastructure.Identity;

public class JwtService : IJwtService
{
    private readonly JwtServiceConfiguration _configuration;

    public JwtService(IConfigureOptions<JwtServiceConfiguration> config)
    {
        config.Configure(_configuration = new JwtServiceConfiguration());    
    }

    public IJwtBuilder GetJwtBuilder()
    {
        return new JwtBuilder(_configuration);
    }

    public class JwtBuilder : IJwtBuilder
    {
        private readonly List<Claim> _claims;
        private readonly JwtServiceConfiguration _configuration;

        public JwtBuilder(JwtServiceConfiguration config)
        {
            _claims = new List<Claim>();
            _configuration = config;
        }

        public IJwtBuilder AddRole(string role)
        {
            _claims.Add(new Claim(ClaimTypes.Role, role));
            return this;
        }

        public IJwtBuilder AddId(string id)
        {
            _claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
            return this;
        }

        public string Build(TimeSpan lifeTime)
        {
            var jwt = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: _claims,
                expires: DateTime.UtcNow.Add(lifeTime),
                signingCredentials: new SigningCredentials(_configuration.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
