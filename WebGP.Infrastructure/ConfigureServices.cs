using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.Common.Configs;
using WebGP.Infrastructure.DataBase;
using WebGP.Infrastructure.Identity;
using WebGP.Infrastructure.SelfDatabase;

namespace WebGP.Infrastructure;

public static class ConfigureServices
{
    private static IServiceCollection AddTypedDatabaseGp<TContext, TContextImplementation>(this IServiceCollection services,
        IConfiguration configuration,
        string key) 
        where TContext : IContext
        where TContextImplementation : DbContext, TContext
    {
        var connectionStringGp = configuration.GetRequiredSection($"DataBase:{key}").Get<DBConfig>()!.GetConnectionString();
        Console.WriteLine($"Add typed {key}: {connectionStringGp}");

        return services.AddDbContext<TContext, TContextImplementation>(options =>
        {
            Console.WriteLine($"Setup {key}: {connectionStringGp}");
            options
                .UseMySql(connectionStringGp, ServerVersion.Create(10, 6, 12, ServerType.MariaDb));
        });
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionStringSelf = configuration.GetRequiredSection("DataBase:Self").Get<DBConfig>()!.GetConnectionString();
        var connectionStringDonate = configuration.GetRequiredSection("DataBase:Donate").Get<DBConfig>()!.GetConnectionString();

        return services
            .AddJwtService(options =>
            {
                options.Key = configuration.GetValue<string>("Jwt:Key")!;
                options.Audience = configuration.GetValue<string>("Jwt:Audience")!;
                options.Issuer = configuration.GetValue<string>("Jwt:Issuer")!;
            })

            .AddTypedDatabaseGp<IContextGPO, DbContextGPO>(configuration, "GPO")
            .AddTypedDatabaseGp<IContextGPC, DbContextGPC>(configuration, "GPC")
            
            .AddDbContext<DonateDbContext>(options => options
                .UseMySql(connectionStringDonate, ServerVersion.Create(10, 6, 12, ServerType.MariaDb)))
            .AddDbContext<SelfDbContext>(options => options
                .UseMySql(connectionStringSelf, ServerVersion.Create(10, 6, 12, ServerType.MariaDb)))

            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IOnlineRepository, OnlineRepository>()
            .AddScoped<IDonateRepository, DonateRepository>()
            .AddScoped<IDiscordRepository, DiscordRepository>();
    }

    private static IServiceCollection AddJwtService(this IServiceCollection services,
        Action<JwtServiceConfiguration> options)
        => services.AddTransient<IJwtService, JwtService>().Configure(options);
}