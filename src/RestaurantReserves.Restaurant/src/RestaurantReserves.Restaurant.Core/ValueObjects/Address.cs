namespace RestaurantReserves.Restaurant.Core.ValueObjects
{
    public class Address
    {
        public string AddressName { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string Cep { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zone { get; private set; }

        public Address() { }

        public Address(string addressName, string number, string complement, string neighborhood, string cep, string city, string state, string zone = null)
        {
            AddressName = addressName;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            Cep = cep;
            City = city;
            State = state;
            Zone = zone;
        }
    }
}
