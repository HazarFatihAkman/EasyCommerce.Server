using EasyCommerce.Server.Shared.Common.TokenManager;
using EasyCommerce.Server.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EasyCommerce;

public static class AuthenticationExtensions
{
    public static void AddAuthentications(this IServiceCollection services, UserOptions userOptions)
    {
        services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(userOptions.ApiKey, userOptions.AddMonths));

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(userOptions.ApiKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}
