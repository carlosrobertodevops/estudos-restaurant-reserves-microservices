namespace Restaurant.Core.UseCases.DeleteRestaurant
{
    public class DeleteUserEvent : Event
    {
        public string Username { get; private set; }

        public DeleteUserEvent(string username, Guid correlationId) : base(correlationId)
        {
            Username = username;
        }
    }
}
