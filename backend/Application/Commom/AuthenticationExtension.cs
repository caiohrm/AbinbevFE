using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Application.Commom;
using Microsoft.Extensions.Configuration;
using CrossCutting.Commom;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Application.Commom
{
	public static class AuthenticationExtension
	{
		public static IServiceCollection AddAuthenticationHandler(this IServiceCollection services,IConfiguration configuration)
		{
            services.AddScoped<IJwtManager, JwtManager>();

            var secretKey = configuration["Jwt:SecretKey"]?.ToString();
            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("Private key is null");

            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(x =>
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
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
	}
}

