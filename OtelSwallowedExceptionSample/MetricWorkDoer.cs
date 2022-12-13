using System.Diagnostics.Metrics;

namespace OtelSwallowedExceptionSample;

public sealed class MetricWorkDoer : WorkDoerDecorator
{
    private readonly Counter<int> counter;

    public MetricWorkDoer(IWorkDoer workDoer, Counter<int> counter)
        : base(workDoer)
    {
        this.counter = counter;
    }

    public override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        await base.DoWorkAsync(cancellationToken);
        this.counter.Add(1);
    }
}
