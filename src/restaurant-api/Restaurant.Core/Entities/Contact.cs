namespace Restaurant.Core.Entities
{
    public class Contact : BaseEntity
    {
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }

        public Restaurant Restaurant { get; private set; }
        public Guid RestaurantId { get; private set; }

        public Contact(string phoneNumber, string email, Restaurant restaurant)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            Restaurant = restaurant;
            RestaurantId = restaurant.Id;

            Validate();
        }

        protected Contact()
        {

        }

        private void Validate()
        {

        }
    }
}
