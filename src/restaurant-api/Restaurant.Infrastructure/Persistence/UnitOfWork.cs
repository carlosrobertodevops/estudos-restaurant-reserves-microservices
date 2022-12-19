namespace Restaurant.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _context;

        public IRestaurantRepository Restaurant { get; }

        public UnitOfWork(IDatabaseContext context,
                          IRestaurantRepository restaurant)
        {
            _context = context;
            Restaurant = restaurant;
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
            {
                _context.Dispose();
                Restaurant.Dispose();
            }
        }
    }
}
