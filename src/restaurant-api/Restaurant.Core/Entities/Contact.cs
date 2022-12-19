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
            if (!PhoneIsFilled() && !EmailIsFilled())
            {
                throw new BusinessException(GenerateValidationErrors(), "Invalid contact");
            }

            //TODO:
            //Add email regex validation
            //Add phone regex validation
        }

        private IDictionary<string, string[]> GenerateValidationErrors()
        {
            var errors = new Dictionary<string, string[]>();

            if(!PhoneIsFilled())
            {
                errors.Add("'Phone'", new string[2] { "'Phone' must not be empty", "At least one of both needs to be filled" });
            }

            if (!EmailIsFilled())
            {
                errors.Add("'Email'", new string[2] { "'Email' must not be empty", "At least one of both needs to be filled" });
            }

            return errors;
        }

        private bool PhoneIsFilled()
        {
            return !string.IsNullOrWhiteSpace(PhoneNumber);
        }

        private bool EmailIsFilled()
        {
            return !string.IsNullOrWhiteSpace(Email);
        }
    }
}
