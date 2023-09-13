namespace multi_hosp_demo.JobQueues;

public class JobA : IJob
{
    public string InpatId { get; set; }
    public string RecordId { get; set; }
}

public class JobAHandler : IJobHandler<JobA>
{
    public async Task ExecuteAsync(JobA command, CancellationToken ct)
    {
        await Task.Delay(2000);
        Console.WriteLine("exec jobA: {0} {1}", command.InpatId, command.RecordId);
    }
}