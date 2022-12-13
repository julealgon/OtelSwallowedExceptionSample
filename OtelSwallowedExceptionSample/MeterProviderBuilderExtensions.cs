using OpenTelemetry.Metrics;
using System.Diagnostics.Metrics;

namespace OtelSwallowedExceptionSample;

public static class MeterProviderBuilderExtensions
{
    public static MeterProviderBuilder AddCustomMetricThatUsedToWork(this MeterProviderBuilder meterProviderBuilder)
    {
        const string meterName = "my-worker-meter";

        meterProviderBuilder
            .GetServices()
            .AddSingleton(_ => new Meter(meterName))
            .AddSingleton(p =>
            {
                var meter = p.GetRequiredService<Meter>();

                return meter.CreateCounter<int>("work-count", unit: "work");
            })
            .Decorate<IWorkDoer, MetricWorkDoer>();

        return meterProviderBuilder.AddMeter(meterName);
    }

    //public static MeterProviderBuilder AddCustomMetricWorkaround(
    //    this MeterProviderBuilder meterProviderBuilder,
    //    IHostEnvironment hostEnvironment)
    //{
    //    const string meterName = "my-worker-meter";

    //    if (!hostEnvironment.IsDevelopment())
    //    {
    //        return meterProviderBuilder;
    //    }

    //    return meterProviderBuilder
    //        .ConfigureServices(
    //            services => services
    //                .AddSingleton(_ => new Meter(meterName))
    //                .AddSingleton(p =>
    //                {
    //                    var meter = p.GetRequiredService<Meter>();

    //                    return meter.CreateCounter<int>("work-count", unit: "work");
    //                })
    //                .Decorate<IWorkDoer, MetricWorkDoer>())
    //        .AddMeter(meterName);
    //}
}
