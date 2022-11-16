namespace Restaurant.Infrastructure.Persistence.Context
{
    public interface IDatabaseContext : IDisposable
    {
        Task<bool> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> AnyPendingMigrationsAsync();
        Task MigrateAsync();
    }
}
