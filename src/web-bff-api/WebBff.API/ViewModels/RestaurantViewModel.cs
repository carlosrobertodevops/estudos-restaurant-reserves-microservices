
using System.Text.Json.Serialization;

namespace WebBff.API.ViewModels
{
    public class RestaurantViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Description { get; set; }
        public int? TotalTables { get; set; }
        public bool? Enabled { get; set; }
        public RestaurantAddressViewModel Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<RestaurantDayOfWorkViewModel> DaysOfWork { get; set; }
        public IEnumerable<RestaurantContactViewModel> Contacts { get; set; }
    }

    public class RestaurantAddressViewModel
    {
        public string FullAddress { get; set; }
        public string PostalCode { get; set; }
        public int? Number { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string Neighborhood { get; set; }
        public string Zone { get; set; }
        public string City { get; set; }
    }

    public class RestaurantContactViewModel
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class RestaurantDayOfWorkViewModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DayOfWeek DayOfWeek { get; set; }
        public int OpensAt { get; set; }
        public int ClosesAt { get; set; }
    }
}
