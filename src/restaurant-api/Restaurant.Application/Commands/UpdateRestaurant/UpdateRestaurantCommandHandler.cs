namespace Restaurant.Application.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IUpdateRestaurantCommandHandler<UpdateRestaurantCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;

        public UpdateRestaurantCommandHandler(IMapper mapper,
                                              IUnitOfWork uow,
                                              ILogger<UpdateRestaurantCommandHandler> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
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

            if (!await _uow.SaveChangesAsync())
            {
                _logger.LogWarning("Unable to create restaurant", request);

                throw new InfrastructureException("Unable to create restaurant", request.CorrelationId);
            }

            return Unit.Value;
        }
    }
}
