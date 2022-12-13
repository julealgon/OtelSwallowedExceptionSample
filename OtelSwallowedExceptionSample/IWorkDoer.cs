namespace OtelSwallowedExceptionSample;

public interface IWorkDoer
{
    Task DoWorkAsync(CancellationToken cancellationToken);
}
