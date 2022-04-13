using DesafioDev.Core.DomainObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioDev.WebAPI.Configuration
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurationAppSettings = configuration.GetSection("TokenConfiguration");
            services.Configure<TokenConfiguration>(tokenConfigurationAppSettings);

            var tokenConfiguration = tokenConfigurationAppSettings.Get<TokenConfiguration>();

            services.AddSingleton(tokenConfigurationAppSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfiguration.Issuer,
                    ValidAudience = tokenConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                            .RequireAuthenticatedUser().Build());
            });

            return services;
        }
    }
}
