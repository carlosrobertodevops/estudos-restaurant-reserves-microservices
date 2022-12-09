namespace Restaurant.Infrastructure.Persistence
{
    public static class QueriesExtensions
    {
        public static string GetRestaurantsContacts = @"SELECT *
                                                        FROM  Contacts NOLOCK
                                                        WHERE RestaurantId = @restaurantId";

        public static string GetRestaurantsDaysOfWork = @"SELECT *
                                                        FROM  DaysOfWork NOLOCK
                                                        WHERE RestaurantId = @restaurantId";

        public static string GetRestaurantsByAddressPaginated = string.Empty;
        public static string GetRestaurantsByNamePaginated = string.Empty;
        public static string GetRestaurantsPaginated = string.Empty;       
    }
}
