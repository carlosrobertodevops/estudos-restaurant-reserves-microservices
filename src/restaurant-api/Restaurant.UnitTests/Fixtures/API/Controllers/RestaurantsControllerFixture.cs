namespace Restaurant.UnitTests.Fixtures.API.Controllers
{
    public class RestaurantsControllerFixture
    {
        public RestaurantsController GenerateValid(RestaurantViewModel restaurant)
        {
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<GetRestaurantByIdQuery>()).Returns(restaurant);

            return new RestaurantsController(mediator);
        }

        public RestaurantsController GenerateInvalid(bool invalidRestaurant)
        {
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<GetRestaurantByIdQuery>()).ThrowsAsync(new NotFoundException());

            if (invalidRestaurant)
            {
                mediator.Send(Arg.Any<CreateRestaurantCommand>()).ThrowsAsync(new BusinessException("Invalid restaurant"));
            }

            return new RestaurantsController(mediator);
        }
    }
}
