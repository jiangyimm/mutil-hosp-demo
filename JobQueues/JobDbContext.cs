using Microsoft.EntityFrameworkCore;
using multi_hosp_demo.Entities;

namespace multi_hosp_demo.JobQueues;

public class JobDbContext : DbContext
{
    public JobDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<JobRecord> JobRecords { get; set; }
}