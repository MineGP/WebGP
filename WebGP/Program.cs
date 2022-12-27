using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebGP.Application;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.DataBase;
using WebGP.Interfaces.Config;
using WebGP.Middlewares;
using WebGP.Models.Config;

public class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args
            });

            IJwtConfig jwtConfig = builder.Configuration.GetRequiredSection("JWT").Get<JwtConfig>()!;

            IDBConfig dbCofigSelf = builder.Configuration.GetRequiredSection("DataBase:Self").Get<DBConfig>()!;
            IDBConfig dbCofigGP = builder.Configuration.GetRequiredSection("DataBase:GP").Get<DBConfig>()!;

            string connetionStringSelf = dbCofigSelf.GetConnectionString();
            string connetionStringGP = dbCofigGP.GetConnectionString();

            builder.Services.AddSingleton(jwtConfig);
            builder.Services.AddScoped<ITimedRepository, TimedRepository>(v => new TimedRepository(connetionStringSelf));
            builder.Services.AddApplicationServices();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

            builder.Services.AddSwaggerGen(options =>
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

            builder.Services.AddSingleton(Log.Logger);
            builder.Host.UseSerilog();

            WebApplication app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.Map("/", async _v => _v.Response.Redirect("swagger/index.html"));
                app.Map("/CustomLogin", (string newIssuer, string newAudience) =>
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Role, "admin") };
                    var jwt = new JwtSecurityToken(
                            issuer: newIssuer,
                            audience: newAudience,
                            expires: DateTime.MaxValue,
                            claims: claims,
                            signingCredentials: new SigningCredentials(jwtConfig.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                });
                app.Map("/DebugLogin", () =>
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Role, "admin") };
                    var jwt = new JwtSecurityToken(
                            issuer: jwtConfig.Issuer,
                            audience: jwtConfig.Audience,
                            expires: DateTime.UtcNow + TimeSpan.FromDays(360),
                            claims: claims,
                            signingCredentials: new SigningCredentials(jwtConfig.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                });
            }
            app.MapControllers();
            app.Run();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Application terminater unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
