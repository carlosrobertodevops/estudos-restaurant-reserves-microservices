namespace Restaurant.Application.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IDeleteRestaurantCommandHandler<DeleteRestaurantCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;

        public DeleteRestaurantCommandHandler(IUnitOfWork uow, 
                                              ILogger<DeleteRestaurantCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _uow.Restaurant.GetByIdAsync(request.Id);

            if (!restaurant.IsValid())
            {
                _logger.LogWarning("Restaurant doesn't exists", request.Id);

                throw new NotFoundException(request.CorrelationId);
            }

            await _uow.Restaurant.DeleteAsync(restaurant);

            if(!await _uow.SaveChangesAsync())
            {
                _logger.LogWarning("Unable to create restaurant", request);

                throw new InfrastructureException("Unable to create restaurant", request.CorrelationId);
            }

            return Unit.Value;
        }
    }
}
