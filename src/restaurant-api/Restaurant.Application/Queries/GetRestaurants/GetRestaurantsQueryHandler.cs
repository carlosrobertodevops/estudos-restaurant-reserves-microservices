namespace Restaurant.Application.Queries.GetRestaurants
{
    public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, IEnumerable<RestaurantViewModel>>
    {
        public Task<IEnumerable<RestaurantViewModel>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Enumerable.Empty<RestaurantViewModel>());
        }
    }
}
