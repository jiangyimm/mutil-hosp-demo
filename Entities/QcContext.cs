using Microsoft.EntityFrameworkCore;
using multi_hosp_demo.MultiHosp;

namespace multi_hosp_demo.Entities
{
    public class QcContext : MultiHospDbContext
    {
        public QcContext(IMultiHospProvider multiHospProvider, DbContextOptions<QcContext> options) : base(multiHospProvider, options)
        {
        }
        public virtual DbSet<ExtDept> ExtDepts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}