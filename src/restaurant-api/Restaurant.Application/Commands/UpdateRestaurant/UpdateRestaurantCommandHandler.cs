namespace Restaurant.Application.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IUpdateRestaurantCommandHandler<UpdateRestaurantCommand>
    {
        private readonly IRestaurantService _service;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;

        public UpdateRestaurantCommandHandler(IRestaurantService service,
                                              IUnitOfWork uow,
                                              ILogger<UpdateRestaurantCommandHandler> logger)
        {
            _service = service;
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            await _service.UpdateRestaurant(request);

            _logger.LogInformation("Restaurant updated", request);

            return Unit.Value;
        }
    }
}
