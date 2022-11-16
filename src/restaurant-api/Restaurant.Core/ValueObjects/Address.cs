namespace Restaurant.Core.ValueObjects
{
    public class Address
    {
        public string FullAddress { get; private set; }
        public string PostalCode { get; private set; }
        public int Number { get; private set; }
        public string State { get; private set; }
        public string Street { get; private set; }
        public string Country { get; private set; }

        public Address(string fullAddress, 
                       string postalCode, 
                       int number, 
                       string state, 
                       string street, 
                       string country)
        {
            FullAddress = fullAddress;
            PostalCode = postalCode;
            Number = number;
            State = state;
            Street = street;
            Country = country;
        }
    }
}
