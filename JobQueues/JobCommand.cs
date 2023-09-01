namespace multi_hosp_demo.JobQueues;

public class JobCommand : ICommand
{
    public string InpatId { get; set; }
    public string RecordId { get; set; }
}

public class JobCommandHandler : ICommandHandler<JobCommand>
{
    public async Task ExecuteAsync(JobCommand command, CancellationToken ct)
    {
        await Task.Delay(2000);
        Console.WriteLine("exec job: {0} {1}", command.InpatId, command.RecordId);
    }
}