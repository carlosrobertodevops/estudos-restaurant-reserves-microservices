using Security.API.Core.Events;

namespace EventBusMessages
{
    public class CreateUserEvent : Event
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

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
