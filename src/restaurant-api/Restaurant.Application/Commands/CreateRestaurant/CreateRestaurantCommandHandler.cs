﻿using Restaurant.Core.UseCases.DeleteRestaurant;
using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Application.Commands.CreateRestaurant
{
    public sealed class CreateRestaurantCommandHandler : ICreateRestaurantCommandHandler<CreateRestaurantCommand, RestaurantViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IMessageBusManager _messageBus;

        public CreateRestaurantCommandHandler(IMapper mapper, 
                                              IUnitOfWork uow, 
                                              ILogger<CreateRestaurantCommandHandler> logger,
                                              IMessageBusManager messageBus)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _messageBus = messageBus;
        }
        
        public async Task<RestaurantViewModel> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = _mapper.Map<RestaurantEntity>(request);

            if(await _uow.Restaurant.ExistsAsync(restaurant))
            {
                _logger.LogWarning("Restaurant already exists", request);

                throw new BusinessException("Restaurant already exists.", request.CorrelationId);
            }

            var accessToken = await _messageBus.CreateRestaurantCredentials(request.AsCreateUserEvent(restaurant.Id), cancellationToken);

            await _uow.Restaurant.CreateAsync(restaurant);

            if(!await _uow.SaveChangesAsync())
            {
                _logger.LogError("Unable to create restaurant", request);

                await _messageBus.DeleteRestaurantCredentials(restaurant.Id, request.CorrelationId, cancellationToken);

                throw new InfrastructureException("Unable to create restaurant.", request.CorrelationId);
            }

            _logger.LogInformation("Restaurant created", new { restaurant, request });

            return _mapper.Map<RestaurantViewModel>(restaurant).WithAccessToken(accessToken.AsAccessToken());
        }
    }
}
