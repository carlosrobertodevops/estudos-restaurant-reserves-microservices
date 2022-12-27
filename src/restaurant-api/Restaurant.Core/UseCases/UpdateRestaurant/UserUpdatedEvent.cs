namespace EventBusMessages
{
    public sealed class UserUpdatedEvent : Event
    {
        public UserUpdatedEvent(Guid correlationId) : base(correlationId)
        {
        }

        public UserUpdatedEvent() : base(Guid.NewGuid())
        {

        }
    }
}
