using Microsoft.AspNetCore.Mvc;

namespace WebGP.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    /*
    public IJwtConfig JWT { get; }
    public ISqlContext SQL { get; }
    public AuthController(IJwtConfig jwt, ISqlContext sql)
    {
        JWT = jwt;
        SQL = sql;
    }
    [Route("login")] public IResponseOrError<IBearerToken> Login([FromQuery(Name = "user")] string user, [FromQuery(Name = "password")] string password)        
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user)
        };
        var jwt = new JwtSecurityToken(
                issuer: JWT.Issuer,
                audience: JWT.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(JWT.Expires),
                signingCredentials: new SigningCredentials(JWT.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new Response<IBearerToken>(new BearerToken(new JwtSecurityTokenHandler().WriteToken(jwt)));
    }
    /*[Authorize] [Route("refresh-token")] public IResponse Refresh()
    {
        /*var jwt = new JwtSecurityToken(
                issuer: JWT.Issuer,
                audience: JWT.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(JWT.Expires),
                signingCredentials: new SigningCredentials(JWT.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new Response<IBearerToken>(new BearerToken(new JwtSecurityTokenHandler().WriteToken(jwt)));*/
    //}
}