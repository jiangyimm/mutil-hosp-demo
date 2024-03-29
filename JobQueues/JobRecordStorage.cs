using multi_hosp_demo.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace multi_hosp_demo.JobQueues;

public class JobRecordStorage : IJobStorageProvider<JobRecord>
{
    private readonly IDbContextFactory<JobDbContext> _dbContextFactory;

    public JobRecordStorage(IDbContextFactory<JobDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task StoreJobAsync(JobRecord r, CancellationToken ct)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        r.JCommand = JsonSerializer.SerializeToElement(r.JobData);
        await dbContext.JobRecords.AddAsync(r, ct);
        await dbContext.SaveChangesAsync(ct);
    }
    public async Task<IEnumerable<JobRecord>> GetNextBatchAsync(PendingJobSearchParams<JobRecord> parameters)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(parameters.CancellationToken);

        var result = await dbContext.JobRecords.AsNoTracking()
                        .Where(parameters.Match)
                        .Take(parameters.Limit)
                        .ToListAsync(parameters.CancellationToken);
        result.ForEach(p => p.JobData = p.JCommand.Deserialize(parameters.TypeOfJob)!);
        return result;
    }

    public async Task MarkJobAsCompleteAsync(JobRecord r, CancellationToken ct)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        await dbContext.JobRecords.Where(p => p.ID == r.ID)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.IsComplete, r.IsComplete)
                                    .SetProperty(x => x.ExecuteAt, r.ExecuteAt), ct);
    }

    public async Task OnHandlerExecutionFailureAsync(JobRecord r, Exception exception, CancellationToken ct)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

        await dbContext.JobRecords.Where(p => p.ID == r.ID)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.ExecuteAfter, DateTime.Now.AddMinutes(1)), ct);
    }

    public async Task PurgeStaleJobsAsync(StaleJobSearchParams<JobRecord> parameters)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(parameters.CancellationToken);

        await dbContext.JobRecords.Where(parameters.Match)
            .ExecuteDeleteAsync(parameters.CancellationToken);
    }
}

