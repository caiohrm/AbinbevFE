using CrossCutting.Services;
using Application.Services;
using CrossCutting.Factories;
using Infrastructure.Repositories;
using CrossCutting.Commom;
using Application.Commom;

namespace AbInbev
{
    public static class IOC
	{
        public static void AddInfrastructureIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmployerService, EmployerService>();
            services.AddScoped<IEmployerRepository, EmployerRepository>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<IPhoneNumberService, PhoneNumberService>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
        }

    }
}

