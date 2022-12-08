namespace Restaurant.Application.Queries.GetRestaurants
{
    public class GetRestaurantsQuery : IGetRestaurantsQuery<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; private set; }
        public int Rows { get; private set; }

        public GetRestaurantsQuery(int page, int rows)
        {
            Page = page;
            Rows = rows;
        }
    }
}
