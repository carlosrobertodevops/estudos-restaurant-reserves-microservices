using EntityRestaurant = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Infrastructure.Persistence.Context
{
    public sealed class SqlServerContext : DbContext, IDatabaseContext
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
            foreach (var entry in ChangeTracker.Entries()
                            .Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null ||
                                            entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("UpdatedAt").CurrentValue = _dateTime.Now;
                    entry.Property("CreatedAt").CurrentValue = _dateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = _dateTime.Now;
                    entry.Property("CreatedAt").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyPendingMigrationsAsync()
        {
            try
            {
                var migrations = await Database.GetPendingMigrationsAsync();

                return migrations.Any();
            }
            catch 
            {
                return false;
            }   
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }
    }
}
