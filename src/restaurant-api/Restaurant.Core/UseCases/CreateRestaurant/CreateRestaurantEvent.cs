namespace EventBusMessages
{
    public class CreateRestaurantEvent : Event
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public CreateRestaurantEvent(string username,
                                     string password,
                                     string firstName,
                                     string lastName,
                                     Guid correlationId) : base(correlationId)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;

            Console.WriteLine($"{typeof(CreateRestaurantEvent).AssemblyQualifiedName}");
        }

        public CreateRestaurantEvent() : base(Guid.NewGuid())
        {

        }
    }
}
