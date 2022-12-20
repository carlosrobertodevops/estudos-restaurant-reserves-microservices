namespace WebBff.API.UseCases.Restaurants.GetByName
{
    public class GetRestaurantsByName : IUseCase<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; set; }
        public int Rows { get; set; }
        public string Name { get; set; }
        public Guid CorrelationId { get; set; }

        public GetRestaurantsByName(int page, int rows, string name, Guid correlationId)
        {
            Page = page;
            Rows = rows;
            Name = name;
            CorrelationId = correlationId;
        }
    }
}
