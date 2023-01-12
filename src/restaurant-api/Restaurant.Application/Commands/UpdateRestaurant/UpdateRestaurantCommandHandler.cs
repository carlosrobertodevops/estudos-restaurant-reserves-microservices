namespace Restaurant.Application.Commands.UpdateRestaurant
{
    public sealed class UpdateRestaurantCommandHandler : IUpdateRestaurantCommandHandler<UpdateRestaurantCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
        private readonly IMessageBusManager _messageBus;

        public UpdateRestaurantCommandHandler(IUnitOfWork uow,
                                              IMapper mapper,
                                              ILogger<UpdateRestaurantCommandHandler> logger,
                                              IMessageBusManager messageBus)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _messageBus = messageBus;
        }

        public async Task<Unit> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _uow.Restaurant.GetByIdAsync(request.Id);

            if (!restaurant.IsValid())
            {
                _logger.LogWarning("Restaurant doesn't exists", request.Id);

                throw new NotFoundException(request.CorrelationId);
            }

            restaurant.Update(request.Restaurant.Name,
                              request.Restaurant.Document,
                              request.Restaurant.Description,
                              request.Restaurant.Address is not null ? _mapper.Map<Address>(request.Restaurant.Address) : null,
                              request.Restaurant.TotalTables,
                              request.Restaurant.Enabled,
                              request.Restaurant.DaysOfWork is not null ? _mapper.Map<ICollection<DayOfWork>>(request.Restaurant.DaysOfWork) : null,
                              request.Restaurant.Contacts is not null ? _mapper.Map<ICollection<Contact>>(request.Restaurant.Contacts) : null);

            await _uow.Restaurant.UpdateAsync(restaurant);

            await _messageBus.RestaurantUpdated(restaurant, request.CorrelationId, cancellationToken);

            _logger.LogInformation("Restaurant updated", request);

            return Unit.Value;
        }
    }
}
