using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebGP;
using WebGP.Application;
using WebGP.Infrastructure;
using WebGP.Interfaces.Config;
using WebGP.Middlewares;
using WebGP.Models.Config;

public class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            IJwtConfig jwtConfig = builder.Configuration.GetRequiredSection("JWT").Get<JwtConfig>()!;

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddServerServices(builder.Configuration);
            
            builder.Host.UseSerilog();

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.Map("/", async _v => _v.Response.Redirect("swagger/index.html"));
                app.Map("/CustomLogin", (string newIssuer, string newAudience) =>
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Role, "admin") };
                    var jwt = new JwtSecurityToken(
                            issuer: newIssuer,
                            audience: newAudience,
                            expires: DateTime.MaxValue,
                            claims: claims,
                            signingCredentials: new SigningCredentials(jwtConfig.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                });
                app.Map("/DebugLogin", () =>
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Role, "admin") };
                    var jwt = new JwtSecurityToken(
                            issuer: jwtConfig.Issuer,
                            audience: jwtConfig.Audience,
                            expires: DateTime.UtcNow + TimeSpan.FromDays(360),
                            claims: claims,
                            signingCredentials: new SigningCredentials(jwtConfig.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                });
            }
            app.MapControllers();
            app.Run();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Application terminater unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
