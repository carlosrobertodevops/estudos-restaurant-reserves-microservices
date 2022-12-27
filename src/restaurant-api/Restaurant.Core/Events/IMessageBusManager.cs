using Restaurant.Core.UseCases.DeleteRestaurant;

namespace Restaurant.Core.Events
{
    public interface IMessageBusManager : IDisposable
    {
        Task<CreateUserEventResponse> CreateRestaurantCredentials(CreateUserEvent createRestaurant, CancellationToken cancellationToken);
        Task DeleteRestaurantCredentials(DeleteUserEvent deleteRestaurant, CancellationToken cancellationToken);
    }
}
