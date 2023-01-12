namespace Restaurant.UnitTests.API
{
    [Collection(nameof(ApiFixtureCollection))]
    public class RestaurantsControllerTests
    {
        private readonly ApiFixture _fixture;

        public RestaurantsControllerTests(ApiFixture fixture)
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
        public void Get_InvalidRequest_ShouldThrow()
        {
            //Arrange
            var page = 0;
            var rows = 0;
            var sut = _fixture.RestaurantsController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.Get(page, rows, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>();
        }

        [Fact]
        public async Task GetById_ExistingRestaurant_ShouldReturnRestaurantViewModel()
        {
            //Arrange
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateValid();

            var sut = _fixture.RestaurantsController.GenerateValid(restaurantViewModel);

            //Act
            var response = await sut.GetById(restaurantViewModel.Id.Value, CancellationToken.None) as ObjectResult;

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
            var sut = _fixture.RestaurantsController.GenerateInvalid();

            //Act
            var act = async () => { await sut.GetById(Guid.NewGuid(), CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public void GetById_InvalidRequest_ShouldThrow()
        {
            //Arrange
            var sut = _fixture.RestaurantsController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.GetById(Guid.NewGuid(), CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>();
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

        [Fact]
        public void GetByName_InvalidRequest_ShouldThrow()
        {
            //Arrange
            var page = 0;
            var rows = 0;
            string name = null;
            var sut = _fixture.RestaurantsController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.GetByName(page, rows, name, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>();
        }

        [Fact]
        public async Task GetByAddress_ExistingRestaurant_ShouldReturnList()
        {
            //Arrange
            var page = 1;
            var rows = 10;
            var restaurantViewModels = _fixture.RestaurantViewModel.GenerateValidCollection(5);
            var city = restaurantViewModels.First().Address.City;
            var neighborhood = restaurantViewModels.First().Address.Neighborhood;
            var zone = restaurantViewModels.First().Address.Zone;
            var expectedRestauranst = restaurantViewModels.Where(r => r.Address.City == city &&
                                                                      r.Address.Neighborhood == neighborhood &&
                                                                      r.Address.Zone == zone);

            var sut = _fixture.RestaurantsController.GenerateValid(restaurants: expectedRestauranst);

            //Act
            var response = await sut.GetByAddress(page, rows, city, neighborhood, zone, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RestaurantViewModel>>();
            response.Value.Should().NotBe(restaurantViewModels);
            response.Value.Should().Be(expectedRestauranst);
        }

        [Fact]
        public async Task GetByAddress_UnexistingRestaurant_ShouldReturnEmptyList()
        {
            //Arrange
            var page = 1;
            var rows = 10;
            var city = "fake city";
            var neighborhood = "fake neighborhood";
            var zone = "fake zone";

            var sut = _fixture.RestaurantsController.GenerateInvalid();

            //Act
            var response = await sut.GetByAddress(page, rows, city, neighborhood, zone, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RestaurantViewModel>>();
            response.Value.Should().Be(Enumerable.Empty<RestaurantViewModel>());
        }

        [Fact]
        public void GetByAddress_InvalidRequest_ShouldThrow()
        {
            //Arrange
            var page = 0;
            var rows = 0;
            string city = null;
            string neighborhood = null;
            string zone = null;
            var sut = _fixture.RestaurantsController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.GetByAddress(page, rows, city, neighborhood, zone, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>();
        }

        [Fact]
        public async Task Post_ValidRequestUnexistentRestaurant_ShouldReturnCreated()
        {
            //Arrange
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateValid();
            var sut = _fixture.RestaurantsController.GenerateValid(restaurantViewModel);

            //Act
            var response = await sut.Post(restaurantViewModel, CancellationToken.None) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Should().BeAssignableTo<CreatedAtActionResult>();
            response.Value.Should().BeAssignableTo<RestaurantViewModel>();
            response.Value.Should().Be(restaurantViewModel);
        }

        [Fact]
        public void Post_ValidRequestExistentRestaurant_ShouldThrow()
        {
            //Arrange
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateInvalid();
            var sut = _fixture.RestaurantsController.GenerateInvalid();

            //Act
            var act = async () => { await sut.Post(restaurantViewModel, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>();
        }

        [Fact]
        public void Post_InvalidRequest_ShouldThrow()
        {
            //Arrange
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateInvalid();
            var sut = _fixture.RestaurantsController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.Post(restaurantViewModel, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>();
        }

        [Fact]
        public async Task Put_ValidRequest_ShouldReturnNoContent()
        {
            //Arrange
            var id = Guid.NewGuid();
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateValid();
            var sut = _fixture.RestaurantsController.GenerateValid();

            //Act
            var response = await sut.Put(id, restaurantViewModel, CancellationToken.None) as NoContentResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            response.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public void Put_ValidRequestUnexistentRestaurant_ShouldThrow()
        {
            //Arrange
            var id = Guid.NewGuid();
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateValid();
            var sut = _fixture.RestaurantsController.GenerateInvalid();

            //Act
            var act = async () => { await sut.Put(id, restaurantViewModel, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<NotFoundException>();
        }

        [Fact]
        public void Put_InvalidRequest_ShouldThrow()
        {
            //Arrange
            var id = Guid.NewGuid();
            var restaurantViewModel = _fixture.RestaurantViewModel.GenerateInvalid();
            var sut = _fixture.RestaurantsController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.Put(id, restaurantViewModel, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>();
        }

        [Fact]
        public async Task Delete_ExistentRestaurant_ShouldReturnNoContent()
        {
            //Arrange
            var id = Guid.NewGuid();
            var sut = _fixture.RestaurantsController.GenerateValid();

            //Act
            var response = await sut.Delete(id, CancellationToken.None) as NoContentResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            response.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public void Delete_UnexistentRestaurant_ShouldThrow()
        {
            //Arrange
            var id = Guid.NewGuid();
            var sut = _fixture.RestaurantsController.GenerateInvalid();

            //Act
            var act = async () => { await sut.Delete(id, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<NotFoundException>();
        }
    }
}
