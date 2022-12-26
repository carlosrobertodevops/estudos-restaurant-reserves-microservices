using Restaurant.Core.UseCases.DeleteRestaurant;

namespace Restaurant.Core.Events
{
    public interface IMessageBusManager : IDisposable
    {
        Task<CreateRestaurantEventResponse> CreateRestaurantCredentials(CreateRestaurantEvent createRestaurant, CancellationToken cancellationToken);
        Task DeleteRestaurantCredentials(DeleteRestaurantEvent deleteRestaurant, CancellationToken cancellationToken);
    }
}
