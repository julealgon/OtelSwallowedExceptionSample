namespace OtelSwallowedExceptionSample;

public sealed class WorkDoer : IWorkDoer
{
    async Task IWorkDoer.DoWorkAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        Console.WriteLine("Did work");
    }
}
