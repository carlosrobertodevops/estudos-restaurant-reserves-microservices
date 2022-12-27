namespace Restaurant.Core.UseCases.DeleteRestaurant
{
    public sealed class DeleteUserEvent : Event
    {
        public DeleteUserEvent(Guid id, Guid correlationId) : base(correlationId)
        {
            AggregateId = id;
        }

        public DeleteUserEvent() : base(Guid.NewGuid())
        {

        }
    }
}
