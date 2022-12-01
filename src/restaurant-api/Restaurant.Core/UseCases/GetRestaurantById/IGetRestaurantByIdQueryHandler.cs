namespace Restaurant.Core.UseCases
{
    public interface IGetRestaurantByIdQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

    }
}
