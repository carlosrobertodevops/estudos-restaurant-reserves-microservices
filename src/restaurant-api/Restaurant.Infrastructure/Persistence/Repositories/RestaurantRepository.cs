using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IDatabaseContext _context;

        public RestaurantRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<RestaurantEntity> CreateAsync(RestaurantEntity restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);

            return restaurant;
        }

        public Task DeleteAsync(RestaurantEntity restaurant)
        {
            _context.Restaurants.Remove(restaurant);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(RestaurantEntity restaurant)
        {
            return await _context.Restaurants.AnyAsync(r => r.Id == restaurant.Id || 
                                                            r.Document == restaurant.Document ||
                                                            r.Name== restaurant.Name);
        }

        public async Task<RestaurantEntity> GetByIdAsync(Guid id)
        {
            var restaurant =  await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

            return restaurant ?? new RestaurantEntity();
        }

        public Task UpdateAsync(RestaurantEntity restaurant)
        {
            _context.Restaurants.Update(restaurant);

            return Task.CompletedTask;
        }
    }
}
