using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EnvioDeEmail.Services.Api.Extensions
{
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Serviço de email - Sistema de controle de envio de emails",
                    Description = "Sistema controla o envio de emails encaminhando as mensagens para o cliente mais adequado a necessidade",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Serviço de Email",
                        Email = "",
                        Url = new Uri("https://portal.fiocruz.br/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments($"{xmlPath}");
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ServicoDeEmail");
            });

            return app;
        }
    }
}
