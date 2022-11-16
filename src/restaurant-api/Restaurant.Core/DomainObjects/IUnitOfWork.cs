namespace Restaurant.Core.DomainObjects
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository Restaurant { get; }
        Task<bool> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
