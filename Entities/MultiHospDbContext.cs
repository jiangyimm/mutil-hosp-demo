using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using multi_hosp_demo.MultiHosp;

namespace multi_hosp_demo.Entities
{
    public class MultiHospDbContext : DbContext
    {
        private string _hospCode;
        public MultiHospDbContext(IMultiHospProvider multiHospProvider, DbContextOptions<QcContext> options) : base(options)
        {
            _hospCode = multiHospProvider?.GetHospCode();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddQueryFilter<MultiHospEntity>(x => x.HospCode == _hospCode);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetChangeTracker();
            return base.SaveChangesAsync();
        }
        public override int SaveChanges()
        {
            SetChangeTracker();
            return base.SaveChanges();
        }

        private void SetChangeTracker()
        {
            ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is MultiHospEntity).ToList().ForEach(e => ((MultiHospEntity)e.Entity).HospCode = _hospCode);
        }
    }

    public static class EntityFrameworkExtensions
    {
        public static void AddQueryFilter<T>(this ModelBuilder modelBuilder,
            Expression<Func<T, bool>> expression)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(T).IsAssignableFrom(entityType.ClrType))
                    continue;

                var parameterType = Expression.Parameter(entityType.ClrType);
                var expressionFilter = ReplacingExpressionVisitor.Replace(
                    expression.Parameters.Single(), parameterType, expression.Body);

                var currentQueryFilter = entityType.GetQueryFilter();
                if (currentQueryFilter != null)
                {
                    var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                        currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                    expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
                }

                var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
                entityType.SetQueryFilter(lambdaExpression);
            }
        }
    }
}