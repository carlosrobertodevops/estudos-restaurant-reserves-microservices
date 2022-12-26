namespace Restaurant.Core.ValueObjects
{
    public class User
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public User()
        {

        }
    }
}
