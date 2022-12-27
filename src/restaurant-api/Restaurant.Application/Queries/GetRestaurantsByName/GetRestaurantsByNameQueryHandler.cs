namespace Restaurant.Application.Queries.GetRestaurantsByName
{
    public sealed class GetRestaurantsByNameQueryHandler : IGetRestaurantsByNameQueryHandler<GetRestaurantsByNameQuery, IEnumerable<RestaurantViewModel>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRestaurantsByNameQueryHandler> _logger;

        public GetRestaurantsByNameQueryHandler(IUnitOfWork uow,
                                                IMapper mapper,
                                                ILogger<GetRestaurantsByNameQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<IEnumerable<RestaurantViewModel>> Handle(GetRestaurantsByNameQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _uow.Restaurant.GetRestaurantsByName(request.Name, request.Page, request.Rows);

            _logger.LogInformation(message: "Restaurants queried by name", new { restaurants, request });

            return _mapper.Map<IEnumerable<RestaurantViewModel>>(restaurants);
        }
    }
}
