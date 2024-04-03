using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Settings;

namespace ServicoDeEmail.Infraestructure.IoC.Extensions
{
    public static class EmailMessageExtension
    {
        public static IServiceCollection AddMessageEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailMessageSettings>(configuration.GetSection("EmailMessageSettings"));
            services.Configure<SecuritySettings>(configuration.GetSection("SecuritySettings"));


            return services;
        }
    }
}
