namespace Restaurant.UnitTests.Fixtures.Application.ViewModels
{
    public class RestaurantViewModelFixture
    {
        public RestaurantViewModel GenerateInvalid()
        {
            return new();
        }

        public RestaurantViewModel GenerateValid()
        {
            return GenerateValidCollection(1).First();
        }

        public IEnumerable<RestaurantViewModel> GenerateValidCollection(int quantity)
        {
            return new Faker<RestaurantViewModel>().RuleFor(r => r.Id, Guid.NewGuid())
                                                   .RuleFor(r => r.Name, f => f.Company.CompanyName())
                                                   .RuleFor(r => r.Description, "fake description")
                                                   .RuleFor(r => r.TotalTables, f => f.Random.Number(1, 50))
                                                   .RuleFor(r => r.Enabled, true)
                                                   .RuleFor(r => r.Address, GenerateValidAddress())
                                                   .RuleFor(r => r.CreatedAt, DateTime.Now)
                                                   .RuleFor(r => r.DaysOfWork, GenerateValidDaysOfWorkCollection())
                                                   .RuleFor(r => r.Contacts, GenerateValidContacts(1))
                                                   .Generate(quantity);
        }

        public AddressViewModel GenerateValidAddress()
        {
            return new Faker<AddressViewModel>().RuleFor(a => a.FullAddress, f => f.Address.FullAddress())
                                                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
                                                .RuleFor(a => a.Number, f=> int.Parse(f.Address.BuildingNumber()))
                                                .RuleFor(a => a.State, f=> f.Address.State())
                                                .RuleFor(a => a.Street, f=> f.Address.StreetName())
                                                .RuleFor(a => a.Country, f => f.Address.Country())
                                                .RuleFor(a => a.Neighborhood, f => f.Address.StreetSuffix())
                                                .RuleFor(a => a.Zone, f => f.Address.OrdinalDirection())
                                                .Generate(1)
                                                .First();
        }

        public IEnumerable<DayOfWorkViewModel> GenerateValidDaysOfWorkCollection()
        {
            var response = new List<DayOfWorkViewModel>(7);

            for (int i = 0; i < 6; i++)
            {
                response.Add(new DayOfWorkViewModel
                {
                    DayOfWeek = (DayOfWeek)i,
                    OpensAt = 8,
                    ClosesAt = 20
                });
            }

            return response;
        }

        public IEnumerable<ContactViewModel> GenerateValidContacts(int quantity)
        {
            return new Faker<ContactViewModel>().RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
                                                .RuleFor(r => r.Email, f => f.Internet.Email())
                                                .Generate(quantity);           
        }
    }
}
