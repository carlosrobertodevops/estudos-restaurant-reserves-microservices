using Restaurant.Core.Entities;

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
																		 R.AddressCity AS City,
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
																FROM (SELECT *
																	  FROM Restaurants (NOLOCK) R
																	  ORDER BY R.Name
																	  OFFSET (@page -1 ) *@rows ROWS 
																	  FETCH NEXT @rows ROWS ONLY) R
																INNER 
																JOIN Contacts (NOLOCK) C
																ON R.Id = C.RestaurantId
																INNER 
																JOIN DaysOfWork (NOLOCK) D  
																ON R.Id = D.RestaurantId
																WHERE LOWER(R.AddressZone) = LOWER(@zone) OR
																      LOWER(R.AddressCity) = LOWER(@city) OR
																      LOWER(R.AddressNeighborhood) = LOWER(@neighborhood)
																ORDER BY R.Name";

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
															   		  R.AddressCity AS City,
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
															   FROM (SELECT *
																	 FROM Restaurants (NOLOCK) R
																	 ORDER BY R.Name
																	 OFFSET (@page -1 ) *@rows ROWS 
																	 FETCH NEXT @rows ROWS ONLY) R
															   INNER 
															   JOIN Contacts (NOLOCK) C
															   ON R.Id = C.RestaurantId
															   INNER 
															   JOIN DaysOfWork (NOLOCK) D  
															   ON R.Id = D.RestaurantId
															   WHERE LOWER(R.Name) LIKE '%' + LOWER(@name) + '%'
															   ORDER BY R.Name";

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
														 		R.AddressCity AS City,
															   	C.Id,
																C.RestaurantId,
															   	C.PhoneNumber,
															   	C.Email,
															   	C.CreatedAt,
															   	C.UpdatedAt,
															   	D.Id,
																D.RestaurantId,
															   	D.DayOfWeek,
															   	D.OpensAt,
															   	D.ClosesAt,
															   	D.CreatedAt,
															   	D.UpdatedAt
														 FROM  (SELECT *
																FROM Restaurants (NOLOCK) R
																ORDER BY R.Name
																OFFSET (@page -1 ) *@rows ROWS 
																FETCH NEXT @rows ROWS ONLY) R
														 INNER 
														 JOIN Contacts (NOLOCK) C
														 ON R.Id = C.RestaurantId
														 INNER 
														 JOIN DaysOfWork (NOLOCK) D  
														 ON R.Id = D.RestaurantId
														 ORDER BY R.Name"
        ;
	}
}

