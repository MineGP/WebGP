using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using WebGP.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            Args = args
        });

        JwtConfig config = builder.Configuration.GetRequiredSection("JWT").Get<JwtConfig>()!;

        builder.Services.AddSingleton(config);

        builder.Services.AddControllersWithViews();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = config.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = config.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = config.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}