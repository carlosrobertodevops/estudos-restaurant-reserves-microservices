namespace Restaurant.Application.Queries.GetRestaurants
{
    public class GetRestaurantsQueryHandler : IGetRestaurantsQueryHandler<GetRestaurantsQuery, IEnumerable<RestaurantViewModel>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRestaurantsQueryHandler> _logger;

        public GetRestaurantsQueryHandler(IUnitOfWork uow,
                                          IMapper mapper,
                                          ILogger<GetRestaurantsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantViewModel>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _uow.Restaurant.GetPaginatedRestaurants(request.Page, request.Rows);

            _logger.LogInformation(message: "Restaurants queried", new {restaurants, request});

            return _mapper.Map<IEnumerable<RestaurantViewModel>>(restaurants);
        }
    }
}
