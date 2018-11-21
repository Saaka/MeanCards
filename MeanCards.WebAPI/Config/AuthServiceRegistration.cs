using MeanCards.Configuration;
using MeanCards.ViewModel.Auth;
using MeanCards.WebAPI.Services;
using MeanCards.WebAPI.Services.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace MeanCards.WebAPI.Config
{
    public static class AuthServiceRegistration
    {
        public static IServiceCollection AddJwtTokenBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration[ConfigurationProperties.Auth.Secret];
            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(x =>
               {
                   x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),

                       ValidateIssuer = true,
                       ValidIssuer = configuration[ConfigurationProperties.Auth.Issuer],

                       ValidateAudience = false,

                   };
               });

            services
                .AddTransient<IJwtTokenFactory, JwtTokenFactory>()
                .AddTransient<IRequestValidator<RegisterUserRequest>, UserRegistrationValidator>()

                .AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }
    }
}
