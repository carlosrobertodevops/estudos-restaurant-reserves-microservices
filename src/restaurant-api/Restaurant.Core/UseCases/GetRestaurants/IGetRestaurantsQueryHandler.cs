namespace Restaurant.Core.UseCases
{
    public interface IGetRestaurantsQueryHandler<TRequest, TResponse>  : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {

    }
}
