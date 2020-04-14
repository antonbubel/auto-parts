namespace AutoParts.Data.EF
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using Model.Results;

    public interface IDatabaseContext : IDisposable
    {
        DbSet<TEntity> DbSet<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        CommitResult Commit();

        Task<CommitResult> CommitAsync();
    }
}
