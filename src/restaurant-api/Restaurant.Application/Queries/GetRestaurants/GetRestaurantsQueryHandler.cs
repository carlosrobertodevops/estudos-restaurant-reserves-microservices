namespace Restaurant.Application.Queries.GetRestaurants
{
    public class GetRestaurantsQueryHandler : IGetRestaurantsQueryHandler<GetRestaurantsQuery, IEnumerable<RestaurantViewModel>>
    {
        public Task<IEnumerable<RestaurantViewModel>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Enumerable.Empty<RestaurantViewModel>());
        }
    }
}
