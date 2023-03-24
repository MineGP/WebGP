using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using WebGP.Application.Common.Interfaces;
using WebGP.Interfaces.Config;
using WebGP.Models.Config;

namespace WebGP
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServerServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSingleton(Log.Logger);

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            IJwtConfig jwtConfig = configuration.GetRequiredSection("JWT").Get<JwtConfig>()!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtConfig.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = jwtConfig.GetSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddAuthorizationBuilder()
                .AddPolicy("full_access", policy => policy
                    .RequireAssertion(context =>
                        context.User.IsInRole("admin")))
                .AddPolicy("script_access", policy => policy
                    .RequireAssertion(context =>
                        context.User.IsInRole("admin") ||
                        context.User.IsInRole("script")))
                .AddPolicy("query_access", policy => policy
                    .RequireAssertion(context =>
                        context.User.IsInRole("admin") ||
                        context.User.IsInRole("query")));

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}
