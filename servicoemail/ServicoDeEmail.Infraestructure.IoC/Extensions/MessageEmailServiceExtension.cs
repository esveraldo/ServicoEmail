using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Mail;
using ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Settings;

namespace ServicoDeEmail.Infraestructure.IoC.Extensions
{
    public static class MessageEmailServiceExtension
    {
        public static IServiceCollection AddMessageService(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<SendMail>();

            return services;
        }
    }
}
