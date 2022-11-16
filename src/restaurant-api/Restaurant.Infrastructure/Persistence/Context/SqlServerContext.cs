using EntityRestaurant = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Infrastructure.Persistence.Context
{
    public class SqlServerContext : DbContext, IDatabaseContext
    {
        private readonly IDateTimeProvider _dateTime;

        public DbSet<EntityRestaurant> Restaurants { get; set; }
        public DbSet<DayOfWork> DaysOfWork { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public SqlServerContext(DbContextOptions options,
                                IDateTimeProvider dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Database.BeginTransactionAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyPendingMigrationsAsync()
        {
            var migrations = await Database.GetPendingMigrationsAsync();

            return migrations.Any();
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }
    }
}
