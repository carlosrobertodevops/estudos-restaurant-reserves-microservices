namespace Restaurant.Infrastructure.Persistence
{
    public static class QueriesExtensions
    {
        public static string GetRestaurantsContacts => @"SELECT *
                                                        FROM  Contacts NOLOCK
                                                        WHERE RestaurantId = @restaurantId";

        public static string GetRestaurantsDaysOfWork => @"SELECT *
                                                        FROM  DaysOfWork NOLOCK
                                                        WHERE RestaurantId = @restaurantId";

        public static string GetRestaurantsByAddressPaginated => @"SELECT R.Id, 
																		 R.Name, 
																		 R.Description, 
																		 R.TotalTables,
																		 R.Enabled,
																		 R.CreatedAt,
																		 R.UpdatedAt,
																		 R.Document,
																		 R.AddressFullAddress AS FullAddress, 
																		 R.AddressPostalCode AS PostalCode, 
																		 R.AddressNumber AS Number,
																		 R.AddressCountry AS Country,
																		 R.AddressNeighborhood AS Neighborhood,
																		 R.AddressState AS State,
																		 R.AddressStreet AS Street,
																		 R.AddressZone AS Zone,
																	     C.RestaurantId,
															   		     C.Id,
															   		     C.PhoneNumber,
															   		     C.Email,
															   		     C.CreatedAt,
															   		     C.UpdatedAt,
																	     D.RestaurantId,
															   		     D.Id,
															   		     D.DayOfWeek,
															   		     D.OpensAt,
															   		     D.ClosesAt,
															   		     D.CreatedAt,
															   		     D.UpdatedAt
																FROM Restaurants (NOLOCK) R
																INNER 
																JOIN Contacts (NOLOCK) C
																ON R.Id = C.RestaurantId
																LEFT 
																JOIN DaysOfWork (NOLOCK) D  
																ON R.Id = D.RestaurantId
																WHERE R.AddressZone = @zone OR
																      R.AddressCity = @city OR
																      R.AddressNeighborhood = @neighborhood
																OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        public static string GetRestaurantsByNamePaginated => @"SELECT R.Id, 
															   		  R.Name, 
															   		  R.Description, 
															   		  R.TotalTables,
															   		  R.Enabled,
															   		  R.CreatedAt,
															   		  R.UpdatedAt,
															   		  R.Document,
															   		  R.AddressFullAddress AS FullAddress, 
															   		  R.AddressPostalCode AS PostalCode, 
															   		  R.AddressNumber AS Number,
															   		  R.AddressCountry AS Country,
															   		  R.AddressNeighborhood AS Neighborhood,
															   		  R.AddressState AS State,
															   		  R.AddressStreet AS Street,
															   		  R.AddressZone AS Zone,
																	  C.RestaurantId,
															   		  C.Id,
															   		  C.PhoneNumber,
															   		  C.Email,
															   		  C.CreatedAt,
															   		  C.UpdatedAt,
																	  D.RestaurantId,
															   		  D.Id,
															   		  D.DayOfWeek,
															   		  D.OpensAt,
															   		  D.ClosesAt,
															   		  D.CreatedAt,
															   		  D.UpdatedAt
															   FROM Restaurants (NOLOCK) R
															   INNER 
															   JOIN Contacts (NOLOCK) C
															   ON R.Id = C.RestaurantId
															   LEFT 
															   JOIN DaysOfWork (NOLOCK) D  
															   ON R.Id = D.RestaurantId
															   WHERE R.Name LIKE '%' + @name +'%'
															   OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        public static string GetRestaurantsPaginated => @"SELECT R.Id, 
														 		R.Name, 
														 		R.Description, 
														 		R.TotalTables,
														 		R.Enabled,
														 		R.CreatedAt,
														 		R.UpdatedAt,
														 		R.Document,
														 		R.AddressFullAddress AS FullAddress, 
														 		R.AddressPostalCode AS PostalCode, 
														 		R.AddressNumber AS Number,
														 		R.AddressCountry AS Country,
														 		R.AddressNeighborhood AS Neighborhood,
														 		R.AddressState AS State,
														 		R.AddressStreet AS Street,
														 		R.AddressZone AS Zone,
																C.RestaurantId,
															   	C.Id,
															   	C.PhoneNumber,
															   	C.Email,
															   	C.CreatedAt,
															   	C.UpdatedAt,
																D.RestaurantId,
															   	D.Id,
															   	D.DayOfWeek,
															   	D.OpensAt,
															   	D.ClosesAt,
															   	D.CreatedAt,
															   	D.UpdatedAt
														 FROM Restaurants (NOLOCK) R
														 INNER 
														 JOIN Contacts (NOLOCK) C
														 ON R.Id = C.RestaurantId
														 LEFT 
														 JOIN DaysOfWork (NOLOCK) D  
														 ON R.Id = D.RestaurantId
														 OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
    }
}