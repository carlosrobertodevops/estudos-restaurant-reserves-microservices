namespace EventBusMessages
{
    public sealed class CreateUserEvent : Event
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }

        public CreateUserEvent(Guid aggregateId,
                               string username,
                               string password,
                               string firstName,
                               string lastName,
                               string userType,
                               Guid correlationId) : base(correlationId)
        {
            AggregateId = aggregateId;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            UserType = userType;
        }

        public CreateUserEvent() : base(Guid.NewGuid())
        {

        }
    }
}
