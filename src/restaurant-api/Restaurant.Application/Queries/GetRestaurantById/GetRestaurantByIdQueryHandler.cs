using Restaurant.Core.Entities;

namespace Restaurant.Application.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler : IGetRestaurantByIdQueryHandler<GetRestaurantByIdQuery, RestaurantViewModel>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;

        public GetRestaurantByIdQueryHandler(IUnitOfWork uow, 
                                             IMapper mapper, 
                                             ILogger<GetRestaurantByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RestaurantViewModel> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _uow.Restaurant.GetByIdAsync(request.Id);

            if(!restaurant.IsValid())
            {
                _logger.LogWarning("Restaurant not found", request);

                throw new NotFoundException(request.CorrelationId, "Restaurant not found");
            }

            _logger.LogInformation("Restaurant queried", new { restaurant, request });

            return _mapper.Map<RestaurantViewModel>(restaurant);
        }
    }
}
