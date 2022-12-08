namespace Restaurant.Core.UseCases
{
    public interface IGetRestaurantsByNameQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

    }
}
