namespace Restaurant.Application.Commands.DeleteRestaurant
{
    public sealed class DeleteRestaurantCommandHandler : IDeleteRestaurantCommandHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantService _service;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IMessageBusManager _messageBus;

        public DeleteRestaurantCommandHandler(IRestaurantService service,
                                              IUnitOfWork uow, 
                                              ILogger<DeleteRestaurantCommandHandler> logger,
                                              IMessageBusManager messageBus)
        {
            _service = service;
            _uow = uow;
            _logger = logger;
            _messageBus = messageBus;
        }

        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            await _service.DeleteRestaurant(request);

            await _messageBus.RestaurantDeleted(request.Id, request.CorrelationId, cancellationToken);

            _logger.LogInformation("Restaurant deleted", request);

            return Unit.Value;
        }
    }
}
