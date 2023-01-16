using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.DataBase;
using WebGP.Infrastructure.Scripts;
using WebGP.Models.Config;

namespace WebGP.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connetionStringSelf =
            configuration.GetRequiredSection("DataBase:Self").Get<DBConfig>()!.GetConnectionString();
        var connetionStringGP = configuration.GetRequiredSection("DataBase:GP").Get<DBConfig>()!.GetConnectionString();

        return services
            .AddDbContext<IContext, ApplicationDbContext>(options => options
                .UseMySql(connetionStringGP, ServerVersion.Create(10, 0, 0, ServerType.MariaDb)))
            .AddRpGenerator(options => options.RunCommand = configuration
                .GetValue<string>("RpGenerator:RunCommand")!);
    }

    private static IServiceCollection AddRpGenerator(this IServiceCollection services,
        Action<RpGenerator.Config> options)
        => services
            .AddSingleton<IRpGenerator, RpGenerator>()
            .Configure(options);
}