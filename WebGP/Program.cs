using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebGP.Application;
using WebGP.Infrastructure;
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

            builder.Services.AddSingleton(jwtConfig);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

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
#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
                app.MapGet("/", async _v => _v.Response.Redirect("swagger/index.html"));
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
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
