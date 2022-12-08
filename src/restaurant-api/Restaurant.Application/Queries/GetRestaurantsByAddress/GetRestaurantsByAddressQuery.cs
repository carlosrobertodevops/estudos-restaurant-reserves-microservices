namespace Restaurant.Application.Queries.GetRestaurantsByAddress
{
    public class GetRestaurantsByAddressQuery : IGetRestaurantsByAddressQuery<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; private set; }
        public int Rows { get; private set; }
        public string City { get; private set; }
        public string Neighborhood { get; private set; }
        public string Zone { get; private set; }
        public Guid CorrelationId {get; private set;}

        public GetRestaurantsByAddressQuery(int page, int rows, string city, string neighborhood, string zone)
        {
            Page = page;
            Rows = rows;
            City = city;
            Neighborhood = neighborhood;
            Zone = zone;
            CorrelationId = Guid.NewGuid();
        }
    }
}