namespace RestaurantReserves.Restaurant.Core.DomainObjects
{
    public class Email
    {
        public const int EMAIL_MAX_LEN = 254;
        public const int EMAIL_MIN_LEN = 5;

        public string Address { get; private set; }

        protected Email() { }

        public Email(string address)
        {
            if (!Validate(address)) throw new BusinessException("Invalid e-mail");

            Address = address;
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            
            return regexEmail.IsMatch(email);
        }
    }
}
