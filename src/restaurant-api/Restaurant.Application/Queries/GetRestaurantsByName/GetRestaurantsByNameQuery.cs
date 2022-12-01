namespace Restaurant.Application.Queries.GetRestaurantsByName
{
    public class GetRestaurantsByNameQuery : IGetRestaurantsByNameQuery<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; private set; }
        public int Rows { get; set; }
        public string Name { get; private set; }

        public GetRestaurantsByNameQuery(int page, int rows, string name)
        {
            Page = page;
            Rows = rows;
            Name = name;
        }
    }
}
