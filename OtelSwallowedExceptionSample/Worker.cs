using System.Diagnostics;

namespace OtelSwallowedExceptionSample;

public class Worker : BackgroundService
{
    private readonly IWorkDoer workDoer;
    private readonly ActivitySource activitySource;

    public Worker(IWorkDoer workDoer, ActivitySource activitySource)
    {
        this.workDoer = workDoer;
        this.activitySource = activitySource;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var activity = this.activitySource.StartActivity("work");
            await this.workDoer.DoWorkAsync(stoppingToken);
        }
    }
}