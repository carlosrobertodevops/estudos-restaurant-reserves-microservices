namespace Restaurant.Application.Queries.GetRestaurantsByAddress
{
    public sealed class GetRestaurantsByAddressQueryHandler : IGetRestaurantsByAddressQueryHandler<GetRestaurantsByAddressQuery, IEnumerable<RestaurantViewModel>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRestaurantsByAddressQueryHandler> _logger;

        public GetRestaurantsByAddressQueryHandler(IUnitOfWork uow,
                                                   IMapper mapper,
                                                   ILogger<GetRestaurantsByAddressQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantViewModel>> Handle(GetRestaurantsByAddressQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _uow.Restaurant.GetRestaurantsByAddress(request.Zone,
                                                                            request.City,
                                                                            request.Neighborhood,
                                                                            request.Page,
                                                                            request.Rows);

            _logger.LogInformation(message: "Restaurants queried by address", new { restaurants, request });

            return _mapper.Map<IEnumerable<RestaurantViewModel>>(restaurants);
        }
    }
}
