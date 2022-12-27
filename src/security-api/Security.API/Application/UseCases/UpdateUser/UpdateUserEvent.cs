using Security.API.Core.Events;

namespace EventBusMessages
{
    public class UpdateUserEvent : Event
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UpdateUserEvent(string username,
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

        public UpdateUserEvent() : base(Guid.NewGuid())
        {

        }
    }
}
