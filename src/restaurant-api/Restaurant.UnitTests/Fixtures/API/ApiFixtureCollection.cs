namespace Restaurant.UnitTests.Fixtures.API
{
    [CollectionDefinition(nameof(ApiFixtureCollection))]
    public class ApiFixtureCollection : ICollectionFixture<ApiFixture> { }

    public class ApiFixture : IDisposable
    {
        public RestaurantViewModelFixture RestaurantViewModel { get; private set; }
        public RestaurantsControllerFixture RestaurantsController { get; private set; }

        public ApiFixture()
        {
            RestaurantViewModel = new RestaurantViewModelFixture();
            RestaurantsController = new RestaurantsControllerFixture();
        }

        public void Dispose()
        {
        }
    }
}
