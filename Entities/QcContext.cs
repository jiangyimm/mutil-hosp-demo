using Microsoft.EntityFrameworkCore;

namespace multi_hosp_demo.Entities
{
    public class QcContext : DbContext
    {
        public QcContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<ExtDept> ExtDepts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}