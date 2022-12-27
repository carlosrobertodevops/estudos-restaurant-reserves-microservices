namespace EventBusMessages
{
    public sealed class CreateUserEvent : Event
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public CreateUserEvent(string username,
                               string password,
                               string firstName,
                               string lastName,
                               Guid correlationId) : base(correlationId)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public CreateUserEvent() : base(Guid.NewGuid())
        {

        }
    }
}
