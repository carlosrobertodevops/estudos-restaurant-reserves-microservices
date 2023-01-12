namespace Restaurant.Application.Commands.DeleteRestaurant
{
    public sealed class DeleteRestaurantCommandHandler : IDeleteRestaurantCommandHandler<DeleteRestaurantCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IMessageBusManager _messageBus;

        public DeleteRestaurantCommandHandler(IUnitOfWork uow, 
                                              ILogger<DeleteRestaurantCommandHandler> logger,
                                              IMessageBusManager messageBus)
        {
            _uow = uow;
            _logger = logger;
            _messageBus = messageBus;
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

            await _messageBus.RestaurantDeleted(request.Id, request.CorrelationId, cancellationToken);

            _logger.LogInformation("Restaurant deleted", request);

            return Unit.Value;
        }
    }
}
