namespace Restaurant.Application.Services
{
    public class RestaurantService : IRestaurantService
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
            var restaurant = await _uow.Restaurant.GetByIdAsync(request.Id);

            if (!restaurant.IsValid())
            {
                _logger.LogWarning("Restaurant doesn't exists", request.Id);

                throw new NotFoundException(request.CorrelationId);
            }

            await DeleteRestaurantAndDependencies(restaurant);
        }

        private async Task DeleteRestaurantAndDependencies(Core.Entities.Restaurant restaurant)
        {
            await _uow.BeginTransactionAsync();

            await _uow.Restaurant.DeleteRestaurantDependencies(restaurant);

            await _uow.Restaurant.DeleteAsync(restaurant);

            await _uow.CommitAsync();
        }

        public async Task UpdateRestaurant(UpdateRestaurantCommand request)
        {
            var restaurant = await GetRestaurant(request);

            restaurant.Update(request.Restaurant.Name,
                              request.Restaurant.Document,
                              request.Restaurant.Description,
                              request.Restaurant.Address is not null ? _mapper.Map<Address>(request.Restaurant.Address) : null,
                              request.Restaurant.TotalTables,
                              request.Restaurant.Enabled,
                              request.Restaurant.DaysOfWork is not null ? _mapper.Map<ICollection<DayOfWork>>(request.Restaurant.DaysOfWork) : null,
                              request.Restaurant.Contacts is not null ? _mapper.Map<ICollection<Contact>>(request.Restaurant.Contacts) : null);


            await UpdateRestaurantAndDependencies(request, restaurant);
        }

        private async Task<Core.Entities.Restaurant> GetRestaurant(UpdateRestaurantCommand request)
        {
            var restaurant = await _uow.Restaurant.GetByIdAsync(request.Id);

            if (!restaurant.IsValid())
            {
                _logger.LogWarning("Restaurant doesn't exists", request.Id);

                throw new NotFoundException(request.CorrelationId);
            }

            return restaurant;
        }

        private async Task UpdateRestaurantAndDependencies(UpdateRestaurantCommand request, Core.Entities.Restaurant restaurant)
        {
            await _uow.BeginTransactionAsync();

            if (request.Restaurant.DaysOfWork is not null || request.Restaurant.Contacts is not null)
            {
                await _uow.Restaurant.DeleteRestaurantDependencies(restaurant);
            }

            await _uow.Restaurant.UpdateAsync(restaurant);

            await _uow.CommitAsync();
        }
    }
}
