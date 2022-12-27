using Restaurant.Core.UseCases.DeleteRestaurant;

namespace Restaurant.Core.Events
{
    public interface IMessageBusManager : IDisposable
    {
        Task<CreateUserEventResponse> CreateRestaurantCredentials(CreateUserEvent createRestaurant, CancellationToken cancellationToken);
        Task DeleteRestaurantCredentials(Guid id, Guid correlationId, CancellationToken cancellationToken);
        Task RestaurantDeleted(Guid id, Guid correlationId, CancellationToken cancellationToken);
        Task RestaurantUpdated(Entities.Restaurant restaurant, Guid correlationId, CancellationToken cancellationToken);
    }
}
