using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Core.Repositories
{
    public interface IRestaurantRepository
    {
        Task<bool> ExistsAsync(RestaurantEntity restaurant);
        Task<RestaurantEntity> CreateAsync(RestaurantEntity restaurant);
        Task DeleteAsync(RestaurantEntity restaurant);
        Task<RestaurantEntity> GetByIdAsync(Guid id);
        Task UpdateAsync(RestaurantEntity restaurant);
        Task DeleteRestaurantDependencies(RestaurantEntity restaurant);
        Task<IEnumerable<RestaurantEntity>> GetPaginatedRestaurants(int page, int rows);
        Task<IEnumerable<RestaurantEntity>> GetRestaurantsByAddress(string zone, string city, string neighborhood, int page, int rows);
        Task<IEnumerable<RestaurantEntity>> GetRestaurantsByName(string name, int page, int rows);
    }
}
