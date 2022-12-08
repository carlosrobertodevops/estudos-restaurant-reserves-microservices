namespace Restaurant.Core.UseCases
{
    public interface IGetRestaurantsByAddressQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

    }
}
