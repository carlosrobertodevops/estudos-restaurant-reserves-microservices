using Dapper;
using System.Data.SqlClient;
using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IDatabaseContext _context;
        private readonly SqlConnection _queryDatabaseConnection;

        public RestaurantRepository(IDatabaseContext context, 
                                    SqlConnection queryDatabaseConnection)
        {
            _context = context;
            _queryDatabaseConnection = queryDatabaseConnection;
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

        public async Task DeleteRestaurantDependencies(RestaurantEntity restaurant)
        {
            await _queryDatabaseConnection.OpenAsync();

            var restaurantId = restaurant.Id;

            var contacts = await _queryDatabaseConnection.QueryAsync<Contact>(QueriesExtensions.GetRestaurantsContacts, new { restaurantId });

            _context.Contacts.RemoveRange(contacts);

            var daysOfWork = await _queryDatabaseConnection.QueryAsync<DayOfWork>(QueriesExtensions.GetRestaurantsDaysOfWork, new { restaurantId });

            _context.DaysOfWork.RemoveRange(daysOfWork);

            await _queryDatabaseConnection.CloseAsync();
        }

        public async Task<bool> ExistsAsync(RestaurantEntity restaurant)
        {
            return await _context.Restaurants.AsNoTracking().AnyAsync(r => r.Id == restaurant.Id || 
                                                                           r.Document == restaurant.Document ||
                                                                           r.Name== restaurant.Name);
        }

        public async Task<RestaurantEntity> GetByIdAsync(Guid id)
        {
            var restaurant =  await _context.Restaurants.AsNoTracking()
                                                        .Include(r => r.DaysOfWork)
                                                        .Include(r => r.Contacts)
                                                        .FirstOrDefaultAsync(r => r.Id == id);

            return restaurant ?? new RestaurantEntity();
        }

        public Task UpdateAsync(RestaurantEntity restaurant)
        {
            _context.Restaurants.Update(restaurant);

            return Task.CompletedTask;
        }
    }
}
