namespace Restaurant.Application.Queries.GetRestaurants
{
    public sealed class GetRestaurantsQuery : IGetRestaurantsQuery<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; private set; }
        public int Rows { get; private set; }
        public Guid CorrelationId { get; private set; }

        public GetRestaurantsQuery(int page, int rows, Guid correlationId)
        {
            Page = page;
            Rows = rows;
            CorrelationId = correlationId;
        }
    }
}
