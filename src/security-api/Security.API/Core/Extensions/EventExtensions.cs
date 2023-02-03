using EventBusMessages;

namespace Security.API.Core.Extensions
{
    public static class EventExtensions
    {
        public static UserViewModel AsUserViewModel(this CreateUserEvent createRestaurantEvent)
        {
            return new UserViewModel
            {
                AggregateId = createRestaurantEvent.AggregateId,
                FirstName = createRestaurantEvent.FirstName,
                LastName = createRestaurantEvent.LastName,
                Username = createRestaurantEvent.Username,
                Password = createRestaurantEvent.Password,
                Usertype = createRestaurantEvent.UserType
            };
        }
    }
}
