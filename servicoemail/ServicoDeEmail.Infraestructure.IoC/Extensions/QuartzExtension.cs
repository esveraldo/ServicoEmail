using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ServicoDeEmail.Infraestructure.MessageEmail.Quartz.Quartz;

namespace ServicoDeEmail.Infraestructure.IoC.Extensions
{
    public static class QuartzExtension
    {
        public static IServiceCollection AddQuartsExtension (this IServiceCollection services)
        {
            services.AddQuartz(q => {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("QuartzJob");
                q.AddJob<QuartzJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("QuartzJob-trigger")
                    .WithCronSchedule("0 * * ? * *")); //Job para cada minuto
                    //.WithCronSchedule("0 0/5 * * * ?")); //Job para cada 5 minutos
                    //.WithCronSchedule("0/30 * * * * ? *")); // Job para cada 30 segundos
            });

            return services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
