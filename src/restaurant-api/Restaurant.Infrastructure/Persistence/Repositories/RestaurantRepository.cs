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
        private readonly SqlConnection _databaseConnection;

        public RestaurantRepository(IDatabaseContext context,
                                    SqlConnection queryDatabaseConnection)
        {
            _context = context;
            _databaseConnection = queryDatabaseConnection;
        }

        public async Task<RestaurantEntity> CreateAsync(RestaurantEntity restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);

            return restaurant;
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
            await _databaseConnection.OpenAsync();

            var restaurants = new List<RestaurantEntity>();

            (await _databaseConnection.QueryAsync
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

            await _databaseConnection.CloseAsync();

            return restaurants ?? Enumerable.Empty<RestaurantEntity>();
        }

        public async Task<IEnumerable<RestaurantEntity>> GetRestaurantsByAddress(string zone,
                                                                                 string city,
                                                                                 string neighborhood,
                                                                                 int page,
                                                                                 int rows)
        {
            await _databaseConnection.OpenAsync();

            var restaurants = new List<RestaurantEntity>();

            (await _databaseConnection.QueryAsync<RestaurantEntity, Address, Contact, DayOfWork, RestaurantEntity>
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

            await _databaseConnection.CloseAsync();

            return restaurants ?? Enumerable.Empty<RestaurantEntity>();
        }

        public async Task<IEnumerable<RestaurantEntity>> GetRestaurantsByName(string name, int page, int rows)
        {
            await _databaseConnection.OpenAsync();

            var restaurants = new List<RestaurantEntity>();

            (await _databaseConnection.QueryAsync<RestaurantEntity, Address, Contact, DayOfWork, RestaurantEntity>
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

            await _databaseConnection.CloseAsync();

            return restaurants ?? Enumerable.Empty<RestaurantEntity>();
        }

        public async Task UpdateAsync(RestaurantEntity restaurant)
        {
            await _databaseConnection.OpenAsync();

            using var transaction = await _databaseConnection.BeginTransactionAsync();

            try
            {
                await TryUpdateAsync(restaurant, transaction);

                await OverrideRestaurantDependencies(restaurant, transaction);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw new InfrastructureException("Unable to update restaurant", ex);
            }

            await _databaseConnection.CloseAsync();
        }

        private async Task TryUpdateAsync(RestaurantEntity restaurant, System.Data.Common.DbTransaction transaction)
        {
            await _databaseConnection.ExecuteAsync(QueriesExtensions.UpdateRestaurant, new
            {
                restaurant.Id,
                restaurant.Name,
                restaurant.Document,
                restaurant.Description,
                restaurant.TotalTables,
                restaurant.Enabled,
                restaurant.Address.FullAddress,
                restaurant.Address.PostalCode,
                restaurant.Address.Number,
                restaurant.Address.State,
                restaurant.Address.Street,
                restaurant.Address.Country,
                restaurant.Address.Neighborhood,
                restaurant.Address.Zone,
                restaurant.Address.City
            }, transaction: transaction);
        }

        private async Task OverrideRestaurantDependencies(RestaurantEntity restaurant, System.Data.Common.DbTransaction transaction)
        {
            await _databaseConnection.ExecuteAsync(QueriesExtensions.DeleteRestaurantDependencies, new { restaurant.Id }, transaction: transaction);

            if (restaurant.Contacts.Any())
            {
                await _databaseConnection.ExecuteAsync(QueriesExtensions.CreateContacts(restaurant), transaction: transaction);
            }

            if (restaurant.DaysOfWork.Any())
            {
                await _databaseConnection.ExecuteAsync(QueriesExtensions.CreateDaysOfWork(restaurant), transaction: transaction);
            }
        }

        public async Task DeleteAsync(RestaurantEntity restaurant)
        {
            await _databaseConnection.OpenAsync();

            using var transaction = await _databaseConnection.BeginTransactionAsync();

            try
            {
                await _databaseConnection.ExecuteAsync(QueriesExtensions.DeleteRestaurantDependencies, new { restaurant.Id }, transaction: transaction);

                await _databaseConnection.ExecuteAsync(QueriesExtensions.DeleteRestaurant, new { restaurant.Id }, transaction: transaction);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw new InfrastructureException("Unable to delete restaurant", ex);
            }

            await _databaseConnection.CloseAsync();
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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                _databaseConnection.Dispose();
            }
        }
    }
}
