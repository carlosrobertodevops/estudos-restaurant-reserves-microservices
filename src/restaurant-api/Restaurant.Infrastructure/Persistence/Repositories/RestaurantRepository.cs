using Dapper;
using Restaurant.Core.Entities;
using Restaurant.Core.ValueObjects;
using System;
using System.Data.SqlClient;
using System.Net;
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
                                                                           r.Name == restaurant.Name);
        }

        public async Task<RestaurantEntity> GetByIdAsync(Guid id)
        {
            var restaurant = await _context.Restaurants.AsNoTracking()
                                                        .Where(r => r.Id == id)
                                                        .Include(r => r.Address)
                                                        .Include(r => r.DaysOfWork)
                                                        .Include(r => r.Contacts)
                                                        .FirstOrDefaultAsync();

            return restaurant ?? new RestaurantEntity();
        }

        public async Task<IEnumerable<RestaurantEntity>> GetPaginatedRestaurants(int page, int rows)
        {
            await _queryDatabaseConnection.OpenAsync();

            var restaurants = new List<RestaurantEntity>();

            (await _queryDatabaseConnection.QueryAsync
                                    (
                                        QueriesExtensions.GetRestaurantsPaginated,
                                        (Func<RestaurantEntity, Address, Contact, DayOfWork, RestaurantEntity>)((restaurant, address, contact, dayOfWork) =>
                                        {
                                            var restaurantAdded = restaurants.FirstOrDefault(r => r.Id == restaurant.Id);

                                            if (restaurantAdded is not null)
                                            {
                                                AddPropertiesToRestaurant(address, contact, dayOfWork, restaurantAdded);

                                                return restaurant;
                                            }

                                            BuildRestaurant(restaurant, address, contact, dayOfWork);

                                            restaurants.Add(restaurant);

                                            return restaurant;
                                        }),
                                        splitOn: "Id,FullAddress,Id,Id",
                                        param: new
                                        {
                                            page,
                                            rows
                                        })).AsList();

            await _queryDatabaseConnection.CloseAsync();

            return restaurants ?? Enumerable.Empty<RestaurantEntity>();
        }

        public async Task<IEnumerable<RestaurantEntity>> GetRestaurantsByAddress(string zone,
                                                                                 string city,
                                                                                 string neighborhood,
                                                                                 int page,
                                                                                 int rows)
        {
            await _queryDatabaseConnection.OpenAsync();

            var restaurants = new List<RestaurantEntity>();

            (await _queryDatabaseConnection.QueryAsync<RestaurantEntity, Address, Contact, DayOfWork, RestaurantEntity>
                                    (
                                         QueriesExtensions.GetRestaurantsByAddressPaginated,
                                         (restaurant, address, contact, dayOfWork) =>
                                         {
                                             var restaurantAdded = restaurants.FirstOrDefault(r => r.Id == restaurant.Id);

                                             if (restaurantAdded is not null)
                                             {
                                                 AddPropertiesToRestaurant(address, contact, dayOfWork, restaurantAdded);

                                                 return restaurant;
                                             }

                                             BuildRestaurant(restaurant, address, contact, dayOfWork);

                                             restaurants.Add(restaurant);

                                             return restaurant;
                                         },
                                         splitOn: "Id,FullAddress,Id,Id",
                                         param: new
                                         {
                                             zone,
                                             city,
                                             neighborhood,
                                             page,
                                             rows
                                         })).AsList();

            await _queryDatabaseConnection.CloseAsync();

            return restaurants ?? Enumerable.Empty<RestaurantEntity>();
        }

        public async Task<IEnumerable<RestaurantEntity>> GetRestaurantsByName(string name, int page, int rows)
        {
            await _queryDatabaseConnection.OpenAsync();

            var restaurants = new List<RestaurantEntity>();

            (await _queryDatabaseConnection.QueryAsync<RestaurantEntity, Address, Contact, DayOfWork, RestaurantEntity>
                                     (
                                         QueriesExtensions.GetRestaurantsByNamePaginated,
                                         (restaurant, address, contact, dayOfWork) =>
                                         {
                                             var restaurantAdded = restaurants.FirstOrDefault(r => r.Id == restaurant.Id);

                                             if (restaurantAdded is not null)
                                             {
                                                 AddPropertiesToRestaurant(address, contact, dayOfWork, restaurantAdded);

                                                 return restaurant;
                                             }

                                             BuildRestaurant(restaurant, address, contact, dayOfWork);

                                             restaurants.Add(restaurant);

                                             return restaurant;
                                         },
                                         splitOn: "Id,FullAddress,Id,Id",
                                         param: new
                                         {
                                             name,
                                             page,
                                             rows
                                         })).AsList();

            await _queryDatabaseConnection.CloseAsync();

            return restaurants ?? Enumerable.Empty<RestaurantEntity>();
        }

        public Task UpdateAsync(RestaurantEntity restaurant)
        {
            _context.Restaurants.Update(restaurant);

            return Task.CompletedTask;
        }

        private static void BuildRestaurant(RestaurantEntity restaurant, Address address, Contact contact, DayOfWork dayOfWork)
        {
            if (restaurant.DaysOfWork is null && restaurant.Contacts is null)
            {
                restaurant.Update(daysOfWork: new List<DayOfWork>(), contacts: new List<Contact>());
            }

            restaurant.Update(address: address);

            if (dayOfWork is not null)
            {
                restaurant.DaysOfWork.Add(dayOfWork);
            }

            if (contact is not null)
            {
                restaurant.Contacts.Add(contact);
            }
        }

        private static void AddPropertiesToRestaurant(Address address, Contact contact, DayOfWork dayOfWork, RestaurantEntity restaurantAdded)
        {
            restaurantAdded.Update(address: address);

            if (dayOfWork is not null && !restaurantAdded.DaysOfWork.Any(d => d.Id == dayOfWork.Id))
            {
                restaurantAdded.DaysOfWork.Add(dayOfWork);
            }

            if (contact is not null && !restaurantAdded.Contacts.Any(d => d.Id == contact.Id))
            {
                restaurantAdded.Contacts.Add(contact);
            }
        }
    }
}
