namespace Restaurant.Core.ValueObjects
{
    public sealed class Address
    {
        public string FullAddress { get; private set; }
        public string PostalCode { get; private set; }
        public int Number { get; private set; }
        public string State { get; private set; }
        public string Street { get; private set; }
        public string Country { get; private set; }
        public string Neighborhood { get; private set; }
        public string Zone { get; private set; }
        public string City { get; private set; }

        public Address(string fullAddress, 
                       string postalCode,
                       int number,
                       string state,
                       string street,
                       string country,
                       string neighborhood,
                       string zone,
                       string city)
        {
            FullAddress = fullAddress;
            PostalCode = postalCode;
            Number = number;
            State = state;
            Street = street;
            Country = country;
            Neighborhood = neighborhood;
            Zone = zone;
            City = city;
        }

        public Address()
        {
            FullAddress = string.Empty;
            PostalCode = string.Empty;
            Number = 0;
            State = string.Empty;
            Street = string.Empty;
            Country = string.Empty;
            Neighborhood = string.Empty;
            Zone = string.Empty;
            City = string.Empty;
        }
    }
}
