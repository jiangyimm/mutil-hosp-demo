namespace FastEndpoints;

public interface IJobHandler<TJob>
where TJob : IJob
{
    Task ExecuteAsync(TJob job, CancellationToken ct);
}