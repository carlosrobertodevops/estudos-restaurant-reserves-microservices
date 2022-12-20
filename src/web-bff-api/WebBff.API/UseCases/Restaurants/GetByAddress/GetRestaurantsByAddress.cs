using WebBff.API.ViewModels;

namespace WebBff.API.UseCases.Restaurants.GetByAddress
{
    public class GetRestaurantsByAddress : IUseCase<IEnumerable<RestaurantViewModel>>
    {
        public int? Page { get; private set; }
        public int? Rows { get; private set; }
        public string City { get; private set; }
        public string Neighborhood { get; private set; }
        public string Zone { get; private set; }
        public Guid CorrelationId { get; private set; }

        public GetRestaurantsByAddress(int? page, int? rows, string city, string neighborhood, string zone, Guid correlationId)
        {
            Page = page;
            Rows = rows;
            City = city;
            Neighborhood = neighborhood;
            Zone = zone;
            CorrelationId = correlationId;
        }
    }
}
