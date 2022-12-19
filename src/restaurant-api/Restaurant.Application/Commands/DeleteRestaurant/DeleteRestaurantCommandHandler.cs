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

            _logger.LogInformation("Restaurant deleted", request);

            return Unit.Value;
        }
    }
}
