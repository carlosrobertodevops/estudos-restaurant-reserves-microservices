namespace Restaurant.Application.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery : IRequest<RestaurantViewModel>
    {
        public Guid Id { get; private set; }

        public GetRestaurantByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
