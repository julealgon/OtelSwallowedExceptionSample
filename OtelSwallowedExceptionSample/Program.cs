using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OtelSwallowedExceptionSample;
using System.Diagnostics;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        services => services
            .AddSingleton(_ => new ActivitySource("worker-source"))
            .AddTransient<IWorkDoer, WorkDoer>()
            .AddHostedService<Worker>()
            .AddOpenTelemetryMetrics(c => c
                .Configure((p, builder) =>
                {
                    var env = p.GetRequiredService<IHostEnvironment>();
                    if (env.IsDevelopment()) 
                    {
                        builder.AddCustomMetricThatUsedToWork();
                    }
                })
                //.AddCustomMetricWorkaround()
                .AddConsoleExporter())
            .AddOpenTelemetryTracing(c => c
                .AddSource("worker-source")
                .AddConsoleExporter()))
            //.AddOpenTelemetry()
            //.WithMetrics(c => c
            //    .AddCustomMetricWorkaround()
            //    .AddConsoleExporter())
            //.WithTracing(c => c
            //    .AddSource("worker-source")
            //    .AddConsoleExporter())
            //.StartWithHost())
    .Build();

host.Run();
