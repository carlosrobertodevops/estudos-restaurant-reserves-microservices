namespace Restaurant.Core.UseCases
{
    public interface IDeleteRestaurantCommandHandler<T> : IRequestHandler<T> where T : IRequest
    {

    }
}
