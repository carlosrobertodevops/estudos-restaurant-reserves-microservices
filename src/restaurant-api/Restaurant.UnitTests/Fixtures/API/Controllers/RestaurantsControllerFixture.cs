using Restaurant.Application.Queries.GetRestaurants;
using Restaurant.Application.Queries.GetRestaurantsByName;

namespace Restaurant.UnitTests.Fixtures.API.Controllers
{
    public class RestaurantsControllerFixture
    {
        public RestaurantsController GenerateValid(RestaurantViewModel restaurant = null, IEnumerable<RestaurantViewModel> restaurants = null)
        {
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<GetRestaurantByIdQuery>()).Returns(restaurant);

            mediator.Send(Arg.Any<GetRestaurantsQuery>()).Returns(restaurants);

            mediator.Send(Arg.Any<GetRestaurantsByNameQuery>()).Returns(restaurants);

            return new RestaurantsController(mediator);
        }

        public RestaurantsController GenerateInvalid(bool invalidRequest = false)
        {
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<GetRestaurantByIdQuery>()).ThrowsAsync(new NotFoundException());

            mediator.Send(Arg.Any<GetRestaurantsQuery>()).Returns(Enumerable.Empty<RestaurantViewModel>());

            mediator.Send(Arg.Any<GetRestaurantsByNameQuery>()).Returns(Enumerable.Empty<RestaurantViewModel>());

            mediator.Send(Arg.Any<CreateRestaurantCommand>()).ThrowsAsync(new BusinessException("Invalid restaurant"));

            if (invalidRequest)
            {
                mediator.Send(Arg.Any<GetRestaurantByIdQuery>()).ThrowsAsync(new BusinessException("Invalid request"));

                mediator.Send(Arg.Any<GetRestaurantsQuery>()).ThrowsAsync(new BusinessException("Invalid request"));

                mediator.Send(Arg.Any<GetRestaurantsByNameQuery>()).ThrowsAsync(new BusinessException("Invalid request"));

                mediator.Send(Arg.Any<CreateRestaurantCommand>()).ThrowsAsync(new BusinessException("Invalid request"));
            }

            return new RestaurantsController(mediator);
        }
    }
}
