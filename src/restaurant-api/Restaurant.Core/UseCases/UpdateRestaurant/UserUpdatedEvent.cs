namespace EventBusMessages
{
    public sealed class UserUpdatedEvent : Event
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public UserUpdatedEvent(string firstName,
                                string lastName, 
                                Guid id, 
                                Guid correlationId) : base(correlationId)
        {
            FirstName = firstName;
            LastName = lastName;
            AggregateId = id;
        }

        public UserUpdatedEvent() : base(Guid.NewGuid())
        {

        }
    }
}
