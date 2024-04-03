using Microsoft.Extensions.DependencyInjection;
using ServicoDeEmail.Application.Interfaces;
using ServicoDeEmail.Application.Services;
using ServicoDeEmail.Domain.Interfaces.Repositories;
using ServicoDeEmail.Domain.Interfaces.Services;
using ServicoDeEmail.Domain.Services;
using ServicoDeEmail.Infraestructure.Data.Repositories;

namespace ServicoDeEmail.Infraestructure.IoC.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IEmailDomainService, EmailDomainService>();
            services.AddScoped<IServiceUsersDomainService, ServiceUsersDomainService>();
            services.AddScoped<IConsumerUsersDomainService, ConsumerUsersDomainService>();
            services.AddScoped<IEmailAppService, EmailAppService>();
            services.AddScoped<IServiceUsersAppService, ServiceUsersAppService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ISecurityDomainService, SecurityDomainService>();
            services.AddSingleton<AuxSendMailRepository>();

            return services;
        }
    }
}
