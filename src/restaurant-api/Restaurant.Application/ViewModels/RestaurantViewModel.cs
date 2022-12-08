namespace Restaurant.Application.ViewModels
{
    public class RestaurantViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Description { get; set; }
        public int TotalTables { get; set; }
        public bool Enabled { get; set; }
        public AddressViewModel Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<DayOfWorkViewModel> DaysOfWork { get; set; }
        public IEnumerable<ContactViewModel> Contacts { get; set; }
    }
}
