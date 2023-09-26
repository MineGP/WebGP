using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using WebGP.Application.Common.Interfaces;
using WebGP.Controllers;
using WebGP.Diaka;
using WebGP.Interfaces.Config;
using WebGP.Java;
using WebGP.Middlewares;
using WebGP.Models.Config;
using WebGP.Tests;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    IJwtConfig jwtConfig = builder.Configuration.GetRequiredSection("JWT").Get<JwtConfig>()!;

    builder.Services.AddMvcCore()
        .UseSpecificControllers(typeof(JavaController));

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddServerServices(builder.Configuration);
    builder.Services.AddSingleton<DiakaListener>();
    builder.Services.AddSingleton<IJavaService, JavaService>();
    builder.Services
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
                expires: DateTime.UtcNow + TimeSpan.FromDays(360 * 5),
                claims: claims,
                signingCredentials: new SigningCredentials(jwtConfig.GetSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            return Results.Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        });
    }

    app.MapControllers();
    Task.WaitAny(app.RunAsync(), app.Services.GetRequiredService<DiakaListener>().StartListen(CancellationToken.None));
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}