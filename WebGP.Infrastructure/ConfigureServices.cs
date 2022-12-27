using Microsoft.Extensions.DependencyInjection;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.DataBase;
using WebGP.Interfaces.Config;

namespace WebGP.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IDBConfig self, IDBConfig gp)
        {
            string connetionStringSelf = self.GetConnectionString();
            string connetionStringGP = gp.GetConnectionString();

            services.AddScoped<IOnlineRepository, OnlineRepository>(v => new OnlineRepository(connetionStringSelf));
            services.AddScoped<IDiscordRepository, DiscordRepository>(v => new DiscordRepository(connetionStringSelf));
            return services;
        }
    }
}
