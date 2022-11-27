namespace Restaurant.Application.Queries.GetRestaurantsByAddress
{
    public class GetRestaurantsByAddressQuery : IRequest<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; private set; }
        public int Row { get; private set; }
        public string City { get; private set; }
        public string Neighborhood { get; private set; }
        public string Zone { get; private set; }

        public GetRestaurantsByAddressQuery(int page, int row, string city, string neighborhood, string zone)
        {
            Page = page;
            Row = row;
            City = city;
            Neighborhood = neighborhood;
            Zone = zone;
        }
    }
}