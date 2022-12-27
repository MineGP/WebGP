using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebGP.Application.Common.Behaviours;
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
