using Restaurant.Core.Events;

namespace Restaurant.Core.UseCases.UpdateRestaurant
{
    public class RestaurantUpdatedEvent : Event
    {
        public RestaurantUpdatedEvent() : base(Guid.Empty)
        {

        }
    }
}
