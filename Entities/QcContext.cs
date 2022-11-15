using Microsoft.EntityFrameworkCore;
using multi_hosp_demo.MultiHosp;

namespace multi_hosp_demo.Entities
{
    public class QcContext : DbContext
    {
        private string _hospCode;
        public QcContext(DbContextOptions<QcContext> options, IMultiHospProvider multiHospProvider) : base(options)
        {
            _hospCode = multiHospProvider?.GetHospCode();
        }
        public virtual DbSet<ExtDept> ExtDepts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExtDept>().HasQueryFilter(e => e.HospCode == _hospCode);
        }
    }
}