namespace OtelSwallowedExceptionSample;

public abstract class WorkDoerDecorator : IWorkDoer
{
    private readonly IWorkDoer workDoer;

    protected WorkDoerDecorator(IWorkDoer workDoer)
    {
        this.workDoer = workDoer;
    }

    public virtual Task DoWorkAsync(CancellationToken cancellationToken)
    {
        return workDoer.DoWorkAsync(cancellationToken);
    }
}
