namespace Restaurant.Core.UseCases.DeleteRestaurant
{
    public class DeleteRestaurantEvent : Event
    {
        public string Username { get; private set; }

        public DeleteRestaurantEvent(string username, Guid correlationId) : base(correlationId)
        {
            Username = username;
        }
    }
}
