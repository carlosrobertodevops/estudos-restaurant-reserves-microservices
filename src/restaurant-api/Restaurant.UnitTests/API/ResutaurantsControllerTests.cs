namespace Restaurant.UnitTests.API
{
    [Collection(nameof(ApiFixtureCollection))]
    public class ResutaurantsControllerTests
    {
        private readonly ApiFixture _fixture;

        public ResutaurantsControllerTests(ApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_ExistingRestaurant_ShouldReturnList()
        {
            //Arrange
            var page = 1;
            var rows = 10;
            var restaurantViewModels = _fixture.RestaurantViewModel.GenerateValidCollection(5);
            var sut = _fixture.RestaurantsController.GenerateValid(restaurants: restaurantViewModels);

            //Act
            var response = await sut.Get(page, rows, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RestaurantViewModel>>();
            response.Value.Should().Be(restaurantViewModels);
        }

        [Fact]
        public async Task Get_UnexistingRestaurant_ShouldReturnEmptyList()
        {
            //Arrange
            var page = 1;
            var rows = 10;
            var sut = _fixture.RestaurantsController.GenerateInvalid();

            //Act
            var response = await sut.Get(page, rows, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RestaurantViewModel>>();
            response.Value.Should().Be(Enumerable.Empty<RestaurantViewModel>());
        }

        [Fact]
        public async Task GetById_ExistingRestaurant_ShouldReturnRouteViewModel()
        {
            //Arrange
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateValid();

            var sut = _fixture.RestaurantsController.GenerateValid(restaurantViewModel);

            //Act
            var response = await sut.GetById(restaurantViewModel.Id, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<RestaurantViewModel>();
            response.Value.Should().Be(restaurantViewModel);
        }

        [Fact]
        public void GetById_UnexistingRestaurant_ShouldThrow()
        {
            //Arrange
            var sut = _fixture.RestaurantsController.GenerateInvalid(false);

            //Act
            var act = async () => { await sut.GetById(Guid.NewGuid(), CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetByName_ExistingRestaurant_ShouldReturnList()
        {
            //Arrange
            var page = 1;
            var rows = 10;
            var restaurantViewModels = _fixture.RestaurantViewModel.GenerateValidCollection(5);
            var name = restaurantViewModels.First().Name;
            var expectedRestauranst = restaurantViewModels.Where(r => r.Name == name);
            var sut = _fixture.RestaurantsController.GenerateValid(restaurants: expectedRestauranst);

            //Act
            var response = await sut.GetByName(page, rows, name, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RestaurantViewModel>>();
            response.Value.Should().NotBe(restaurantViewModels);
            response.Value.Should().Be(expectedRestauranst);
        }

        [Fact]
        public async Task GetByName_UnexistingRestaurant_ShouldReturnEmptyList()
        {
            //Arrange
            var page = 1;
            var rows = 10;
            var name = "fake name";
            var sut = _fixture.RestaurantsController.GenerateInvalid();

            //Act
            var response = await sut.GetByName(page, rows, name, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RestaurantViewModel>>();
            response.Value.Should().Be(Enumerable.Empty<RestaurantViewModel>());
        }
    }
}
