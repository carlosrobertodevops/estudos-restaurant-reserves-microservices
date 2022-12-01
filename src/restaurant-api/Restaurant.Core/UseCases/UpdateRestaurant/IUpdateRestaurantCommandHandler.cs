namespace Restaurant.Core.UseCases
{
    public interface IUpdateRestaurantCommandHandler<T> : IRequestHandler<T> where T : IRequest
    {

    }
}
