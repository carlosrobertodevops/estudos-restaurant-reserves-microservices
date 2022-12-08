namespace Restaurant.Core.UseCases
{
    public interface ICreateRestaurantCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

    }
}
