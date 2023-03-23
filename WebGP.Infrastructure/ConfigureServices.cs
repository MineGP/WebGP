using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.Common.Configs;
using WebGP.Infrastructure.DataBase;
using WebGP.Infrastructure.Identity;
using WebGP.Infrastructure.Scripts;
using WebGP.Infrastructure.SelfDatabase;

namespace WebGP.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionStringSelf = configuration.GetRequiredSection("DataBase:Self").Get<DBConfig>()!.GetConnectionString();
        var connectionStringGp = configuration.GetRequiredSection("DataBase:GP").Get<DBConfig>()!.GetConnectionString();

        services.AddJwtService(options =>
        {
            options.Key = configuration.GetValue<string>("Jwt:Key")!;
            options.Audience = configuration.GetValue<string>("Jwt:Audience")!;
            options.Issuer = configuration.GetValue<string>("Jwt:Issuer")!;
        });

        services.AddDbContext<IContext, ApplicationDbContext>(options => options
            .UseMySql(connectionStringGp, ServerVersion.Create(10, 0, 0, ServerType.MariaDb)));
        services.AddDbContext<SelfDbContext>(options => options
            .UseMySql(connectionStringSelf, ServerVersion.Create(10, 0, 0, ServerType.MariaDb)));
        services.AddRpGenerator(options => options.RunCommand = configuration
            .GetValue<string>("RpGenerator:RunCommand")!);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddJwtService(this IServiceCollection services,
        Action<JwtServiceConfiguration> options)
        => services.AddTransient<IJwtService, JwtService>().Configure(options);

    private static IServiceCollection AddRpGenerator(this IServiceCollection services,
        Action<RpGenerator.Config> options)
        => services
            .AddSingleton<IRpGenerator, RpGenerator>()
            .Configure(options);
}