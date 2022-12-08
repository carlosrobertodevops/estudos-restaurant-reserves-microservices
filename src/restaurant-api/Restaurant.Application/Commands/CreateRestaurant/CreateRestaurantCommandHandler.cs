using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Application.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : ICreateRestaurantCommandHandler<CreateRestaurantCommand, RestaurantViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;

        public CreateRestaurantCommandHandler(IMapper mapper, 
                                              IUnitOfWork uow, 
                                              ILogger<CreateRestaurantCommandHandler> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        
        public async Task<RestaurantViewModel> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = _mapper.Map<RestaurantEntity>(request.Restaurant);

            if(await _uow.Restaurant.ExistsAsync(restaurant))
            {
                _logger.LogWarning("Restaurant already exists", request);

                throw new BusinessException("Restaurant already exists.", request.CorrelationId);
            }

            await _uow.Restaurant.CreateAsync(restaurant);

            if(!await _uow.SaveChangesAsync())
            {
                _logger.LogError("Unable to create restaurant", request);

                throw new InfrastructureException("Unable to create restaurant.", request.CorrelationId);
            }

            _logger.LogInformation("Restaurant created", restaurant);

            return _mapper.Map<RestaurantViewModel>(restaurant);
        }
    }
}
