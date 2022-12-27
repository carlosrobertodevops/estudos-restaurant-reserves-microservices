namespace Restaurant.Application.Commands.UpdateRestaurant
{
    public sealed class UpdateRestaurantCommandHandler : IUpdateRestaurantCommandHandler<UpdateRestaurantCommand>
    {
        private readonly IRestaurantService _service;
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
        private readonly IMessageBusManager _messageBus;

        public UpdateRestaurantCommandHandler(IRestaurantService service,
                                              ILogger<UpdateRestaurantCommandHandler> logger,
                                              IMessageBusManager messageBus)
        {
            _service = service;
            _logger = logger;
            _messageBus = messageBus;
        }

        public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _service.UpdateRestaurant(request);

            await _messageBus.RestaurantUpdated(restaurant, request.CorrelationId, cancellationToken);

            _logger.LogInformation("Restaurant updated", request);

            return Unit.Value;
        }
    }
}
