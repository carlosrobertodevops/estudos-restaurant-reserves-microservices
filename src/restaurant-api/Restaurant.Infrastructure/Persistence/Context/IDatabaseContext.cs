using EntityRestaurant = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Infrastructure.Persistence.Context
{
    public interface IDatabaseContext : IDisposable
    {
        public DbSet<EntityRestaurant> Restaurants { get; }
        public DbSet<DayOfWork> DaysOfWork { get; }
        public DbSet<Contact> Contacts { get; }

        Task<bool> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> AnyPendingMigrationsAsync();
        Task MigrateAsync();
    }
}
