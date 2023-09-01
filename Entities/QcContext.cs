using Microsoft.EntityFrameworkCore;

namespace multi_hosp_demo.Entities
{
    public class QcContext : DbContext
    {
        public QcContext(DbContextOptions<QcContext> options) : base(options)
        {
        }
        public virtual DbSet<ExtDept> ExtDepts { get; set; }
        public virtual DbSet<JobTodo> JobTodos { get; set; }
        public virtual DbSet<JobDone> JobDones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}