using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebGP;
using WebGP.Application;
using WebGP.Application.Common.Interfaces;
using WebGP.Diaka;
using WebGP.Infrastructure;
using WebGP.Infrastructure.Common.Configs;
using WebGP.Interfaces.Config;
using WebGP.Java;
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

    bool isDiakaEnable = DiakaListener.IsEnable(builder.Configuration);
    if (isDiakaEnable)
        builder.Services.AddSingleton<DiakaListener>();

    builder.Services.AddSingleton<IJavaService, JavaService>();
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

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Results.Ok(token);
        });
    }

    app.MapControllers();
    List<Task> runTasks = new List<Task> { app.RunAsync() };
    if (isDiakaEnable)
        runTasks.Add(app.Services.GetRequiredService<DiakaListener>().StartListen(CancellationToken.None));
    Task.WaitAny(runTasks.ToArray());
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}