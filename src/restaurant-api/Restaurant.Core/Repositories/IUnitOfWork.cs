namespace Restaurant.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository Restaurant { get; }
        Task<bool> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
