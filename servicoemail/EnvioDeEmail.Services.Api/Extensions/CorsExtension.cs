namespace EnvioDeEmail.Services.Api.Extensions
{
    public static class CorsExtension
    {
        private static string _policeName = "DefaltPolice";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(s => s.AddPolicy(_policeName, builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            return services;
        }

        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(_policeName);
            return app;
        }
    }
}
