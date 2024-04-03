using EnvioDeEmail.Services.Api.Extensions;
using EnvioDeEmail.Services.Api.Middlewares;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Quartz;
using Serilog;
using ServicoDeEmail.Infraestructure.Data.Repositories;
using ServicoDeEmail.Infraestructure.IoC.Extensions;
using System.Diagnostics;
using System.Diagnostics.Metrics;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();



    builder.Services.AddControllers();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddDependencyInjection();
    builder.Services.AddSwaggerDoc();
    builder.Services.AddCorsPolicy();
    builder.Services.AddDbContextConfig(builder.Configuration);
    builder.Services.AddAutoMapperConfig();
    builder.Services.AddMessageService(builder.Configuration);
    builder.Services.AddMessageEmailService(builder.Configuration);
    builder.Services.AddSingleton(map => new AuxEmailRepository(builder.Configuration.GetConnectionString("BDServicoDeEmail")));
    builder.Services.AddQuartsExtension();

    // Custom metrics for the application
    var greeterMeter = new Meter("ServicoDeEmail", "1.0.0");
    var countGreetings = greeterMeter.CreateCounter<int>("emailservice", description: "Serviço de email com broker de mensageria.");

    // Custom ActivitySource for the application
    var greeterActivitySource = new ActivitySource("ServicoDeEmail");

    var tracingOtlpEndpoint = builder.Configuration["OTLP_ENDPOINT_URL"];
    var otel = builder.Services.AddOpenTelemetry();

    // Configure OpenTelemetry Resources with the application name
    otel.ConfigureResource(resource => resource
        .AddService(serviceName: builder.Environment.ApplicationName));

    // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
    otel.WithMetrics(metrics => metrics
        // Metrics provider from OpenTelemetry
        .AddAspNetCoreInstrumentation()
        .AddMeter(greeterMeter.Name)
        // Metrics provides by ASP.NET Core in .NET 7
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        .AddPrometheusExporter());

    // Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
    otel.WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation();
        tracing.AddHttpClientInstrumentation();
        tracing.AddSource(greeterActivitySource.Name);
        if (tracingOtlpEndpoint != null)
        {
            tracing.AddOtlpExporter(otlpOptions =>
            {
                otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
            });
        }
        else
        {
            tracing.AddConsoleExporter();
        }
    });

    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

    var app = builder.Build();

    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseCorsPolicy();

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("Starting host...");

    app.MapPrometheusScrapingEndpoint();

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program {}


