using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Exceptions;
using Restaurant.UnitTests.Fixtures.API;

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
    }
}
