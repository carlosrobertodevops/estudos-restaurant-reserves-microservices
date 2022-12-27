namespace Restaurant.Application.Services
{
    public sealed class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IMapper _mapper;

        public RestaurantService(IUnitOfWork uow, 
                                 ILogger<RestaurantService> logger, 
                                 IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task DeleteRestaurant(DeleteRestaurantCommand request)
        {
            var restaurant = await GetRestaurant(request.Id, request.CorrelationId);

            await _uow.Restaurant.DeleteAsync(restaurant);
        }

        public async Task<Core.Entities.Restaurant> UpdateRestaurant(UpdateRestaurantCommand request)
        {
            var restaurant = await GetRestaurant(request.Id, request.CorrelationId);

            restaurant.Update(request.Restaurant.Name,
                              request.Restaurant.Document,
                              request.Restaurant.Description,
                              request.Restaurant.Address is not null ? _mapper.Map<Address>(request.Restaurant.Address) : null,
                              request.Restaurant.TotalTables,
                              request.Restaurant.Enabled,
                              request.Restaurant.DaysOfWork is not null ? _mapper.Map<ICollection<DayOfWork>>(request.Restaurant.DaysOfWork) : null,
                              request.Restaurant.Contacts is not null ? _mapper.Map<ICollection<Contact>>(request.Restaurant.Contacts) : null);

            await _uow.Restaurant.UpdateAsync(restaurant);

            return restaurant;
        }

        private async Task<Core.Entities.Restaurant> GetRestaurant(Guid id, Guid correlationId)
        {
            var restaurant = await _uow.Restaurant.GetByIdAsync(id);

            if (!restaurant.IsValid())
            {
                _logger.LogWarning("Restaurant doesn't exists", id);

                throw new NotFoundException(correlationId);
            }

            return restaurant;
        }
    }
}
