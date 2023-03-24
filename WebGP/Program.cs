using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebGP;
using WebGP.Application;
using WebGP.Infrastructure;
using WebGP.Interfaces.Config;
using WebGP.Middlewares;
using WebGP.Models.Config;

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

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGet("/", () => Results.Redirect("swagger/index.html"));

    if (app.Environment.IsDevelopment())
    {
        app.MapGet("/debug_login", () =>
        {
            var claims = new List<Claim> { new(ClaimTypes.Role, "admin") };
            var jwt = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                expires: DateTime.UtcNow + TimeSpan.FromDays(360),
                claims: claims,
                signingCredentials: new SigningCredentials(jwtConfig.GetSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            return Results.Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        });
    }

    app.MapControllers();
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}