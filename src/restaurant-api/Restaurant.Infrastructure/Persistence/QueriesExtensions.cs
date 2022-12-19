using Restaurant.Core.Entities;
using System.Net.NetworkInformation;

namespace Restaurant.Infrastructure.Persistence
{
	public static class QueriesExtensions
	{
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

		public static string UpdateRestaurant => @"UPDATE [Restaurants]
												   SET Name = @Name, 
												   	   Document = @Document, 
												   	   TotalTables = @TotalTables, 
												   	   Enabled = @Enabled, 
												   	   AddressFullAddress = @FullAddress, 
												   	   AddressPostalCode = @PostalCode, 
												   	   AddressNumber = @Number, 
												   	   AddressState = @State, 
												   	   AddressStreet = @Street, 
												   	   AddressNeighborhood = @Neighborhood, 
												   	   AddressZone = @Zone, 
												   	   AddressCity = @City  
												   WHERE Id = @Id";

        public static string DeleteRestaurant => @"DELETE FROM [Restaurants] 
												   WHERE Id = @Id";

        public static string DeleteRestaurantDependencies => @"DELETE FROM [Contacts] 
														       WHERE RestaurantId = @Id;
															   DELETE FROM [DaysOfWork] 
														       WHERE RestaurantId = @Id;";

		public static string CreateContacts(Core.Entities.Restaurant restaurant)
		{
			var command = "INSERT INTO [Contacts] (Id, RestaurantId, PhoneNumber, Email, CreatedAt, UpdatedAt) VALUES";

			if (!restaurant.Contacts.Any())
			{
				return string.Empty;
			}

			foreach (var contact in restaurant.Contacts)
			{
				command = $"{command} ({contact.Id.ToString().ValidStringFieldIfNullOrEmpty()},{restaurant.Id.ToString().ValidStringFieldIfNullOrEmpty()},{contact.PhoneNumber.ValidStringFieldIfNullOrEmpty()},{contact.Email.ValidStringFieldIfNullOrEmpty()},{contact.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss").ValidStringFieldIfNullOrEmpty()},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ValidStringFieldIfNullOrEmpty()}),";
			}

            return command[..^1];
        }

        public static string CreateDaysOfWork(Core.Entities.Restaurant restaurant)
		{
			var command = "INSERT INTO [DaysOfWork] (Id, RestaurantId, DayOfWeek, OpensAt, ClosesAt, CreatedAt, UpdatedAt) VALUES";

			if (!restaurant.DaysOfWork.Any())
			{
				return string.Empty;
			}

			foreach (var dayOfWork in restaurant.DaysOfWork)
			{
				command = $"{command} ({dayOfWork.Id.ToString().ValidStringFieldIfNullOrEmpty()},{restaurant.Id.ToString().ValidStringFieldIfNullOrEmpty()},{Enum.GetName(typeof(DayOfWeek), dayOfWork.DayOfWeek).ValidStringFieldIfNullOrEmpty()},{dayOfWork.OpensAt},{dayOfWork.ClosesAt},{dayOfWork.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss").ValidStringFieldIfNullOrEmpty()},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ValidStringFieldIfNullOrEmpty()}),";
			}

			return command[..^1];
        }

        private static string ValidStringFieldIfNullOrEmpty(this string field)
		{
			if (string.IsNullOrWhiteSpace(field))
			{
				return "''";
			}

			return $"'{field}'";
		}
	}
}