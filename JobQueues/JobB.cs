using System.Text.Json.Serialization;

namespace multi_hosp_demo.JobQueues;

public class JobB : IJob
{
    [JsonPropertyName("patient_name")]
    public string PatientName { get; set; }
    [JsonPropertyName("sex")]
    public string Sex { get; set; }
}

public class JobBHandler : IJobHandler<JobB>
{
    public async Task ExecuteAsync(JobB job, CancellationToken ct)
    {
        await Task.Delay(500);
        Console.WriteLine("exec jobB: {0} {1}", job.PatientName, job.Sex);
    }
}