namespace Restaurant.Application.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IDeleteRestaurantCommandHandler<DeleteRestaurantCommand>
    {
        private readonly IRestaurantService _service;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;

        public DeleteRestaurantCommandHandler(IRestaurantService service,
                                              IUnitOfWork uow, 
                                              ILogger<DeleteRestaurantCommandHandler> logger)
        {
            _service = service;
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            await _service.DeleteRestaurant(request);

            if (!await _uow.SaveChangesAsync())
            {
                _logger.LogWarning("Unable to create restaurant", request);

                throw new InfrastructureException("Unable to create restaurant", request.CorrelationId);
            }

            return Unit.Value;
        }
    }
}
