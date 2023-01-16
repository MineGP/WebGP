using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.DataBase;
using WebGP.Infrastructure.Scripts;
using WebGP.Interfaces.Config;
using WebGP.Models.Config;

namespace WebGP.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringSelf = configuration.GetRequiredSection("DataBase:Self").Get<DBConfig>()!.GetConnectionString();
            var connectionStringGp = configuration.GetRequiredSection("DataBase:GP").Get<DBConfig>()!.GetConnectionString();

            return services
                .AddDbContext<IContext, ApplicationDbContext>(options => options
                    .UseMySql(connectionStringGp, ServerVersion.Create(10, 0, 0, ServerType.MariaDb)))
                .AddRpGenerator(options => options.RunCommand = configuration
                    .GetValue<string>("RpGenerator:RunCommand")!);
        }

        private static IServiceCollection AddRpGenerator(this IServiceCollection services, Action<RpGenerator.Config> options)
            => services
            .AddSingleton<IRpGenerator, RpGenerator>()
            .Configure(options);
    }
}
