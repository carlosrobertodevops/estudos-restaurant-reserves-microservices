namespace Restaurant.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _context;
        private IDbContextTransaction _transaction;

        public IRestaurantRepository Restaurant { get; }

        public UnitOfWork(IDatabaseContext context,
                          IRestaurantRepository restaurant)
        {
            _context = context;
            Restaurant = restaurant;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw new InfrastructureException("Error on commiting changes to database", ex);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }
    }
}
