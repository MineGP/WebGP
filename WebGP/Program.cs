using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebGP.Application;
using WebGP.Application.Common.Interfaces;
using WebGP.Infrastructure.DataBase;
using WebGP.Interfaces;
using WebGP.Middlewares;
using WebGP.Models;

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

            IJwtConfig config = builder.Configuration.GetRequiredSection("JWT").Get<JwtConfig>()!;
            string connetionString = builder.Configuration.GetConnectionString("self")!;

            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<ITimedRepository, TimedRepository>(v => new TimedRepository(connetionString));
            builder.Services.AddApplicationServices();

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = config.Issuer,
                        ValidateAudience = true,
                        ValidAudience = config.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = config.GetSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
            builder.Services.AddSwaggerGen();

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

            app.MapControllers();
            if (app.Environment.IsDevelopment())
            {
                app.UseEndpoints(v =>
                {
                    v.MapGet("/", async _v => _v.Response.Redirect("swagger/index.html"));
                });
            }
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
