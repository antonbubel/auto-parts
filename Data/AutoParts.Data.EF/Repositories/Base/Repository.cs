namespace AutoParts.Data.EF.Repositories.Base
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Model.Results;
    using Model.Entities.Base;
    using Model.Repositories.Base;

    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        protected readonly IDatabaseContext context;

        public Repository(IDatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return context.DbSet<TEntity>()
                .AsNoTracking();
        }

        public async Task<TEntity> FindAsync(TKey key)
        {
            return await context.DbSet<TEntity>()
                .FindAsync(key);
        }

        public async Task<TEntity[]> GetAllAsync()
        {
            return await context.DbSet<TEntity>()
                .ToArrayAsync();
        }

        public async Task<OperationResult<TEntity>> CreateAsync(TEntity entity)
        {
            var entityEntry = await context.DbSet<TEntity>()
                .AddAsync(entity);

            var result = await context.CommitAsync();

            return DatabaseResult(result, entityEntry.Entity);
        }

        public async Task<OperationResult<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.DbSet<TEntity>()
                .AddRangeAsync(entities);

            var result = await context.CommitAsync();

            return DatabaseResult(result);
        }

        public async Task<OperationResult<TEntity>> UpdateAsync(TEntity entity)
        {
            var entityEntry = context.DbSet<TEntity>()
                .Attach(entity);

            entityEntry.State = EntityState.Modified;

            var result = await context.CommitAsync();

            return DatabaseResult(result, entityEntry.Entity);
        }

        public async Task<OperationResult<TEntity>> RemoveAsync(TKey key)
        {
            var entity = await FindAsync(key);

            if (entity != null)
            {
                var entityEntry = context.DbSet<TEntity>()
                    .Remove(entity);

                var result = await context.CommitAsync();

                return DatabaseResult(result, entityEntry.Entity);
            }

            return ValidationError($"{typeof(TEntity).Name} entity with id {key} was not found.");
        }

        public async Task<OperationResult<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            context.DbSet<TEntity>()
                .RemoveRange(entities);

            var result = await context.CommitAsync();

            return DatabaseResult(result);
        }

        private OperationResult<TEntity> DatabaseResult(CommitResult commitResult, TEntity entity = null)
        {
            if (commitResult.Success)
            {
                return new OperationResult<TEntity>(OperationStatus.Successful, entity);
            }

            return new OperationResult<TEntity>(OperationStatus.DatabaseError, commitResult.Exception);
        }

        private OperationResult<TEntity> ValidationError(string message)
        {
            return new OperationResult<TEntity>(OperationStatus.ValidationError, message);
        }
    }
}
