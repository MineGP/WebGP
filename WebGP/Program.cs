using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebGP;
using WebGP.Application;
using WebGP.Diaka;
using WebGP.Infrastructure;
using WebGP.Infrastructure.DataBase;
using WebGP.Infrastructure.SelfDatabase;
using WebGP.Interfaces.Config;
using WebGP.Middlewares;
using WebGP.Models.Config;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    IJwtConfig jwtConfig = builder.Configuration.GetRequiredSection("JWT").Get<JwtConfig>()!;

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddServerServices(builder.Configuration);
    builder.Services.AddSingleton<DiakaListener>();
    builder.Services
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    builder.Host.UseSerilog();

    var app = builder.Build();

    /*
    // MIGRATIONS FOR THE MAIN DB ONLY FOR DEV CONTAINERS
    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<SelfDbContext>();
        db.Database.Migrate();
    }
    */

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