namespace Restaurant.Core.Entities
{
    public class DayOfWork : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; private set; }
        public int OpensAt { get; private set; }
        public int ClosesAt { get; private set; }

        public Guid RestaurantId { get; private set; }
        public Restaurant Restaurant { get; private set; }

        public DayOfWork(DayOfWeek dayOfWeek, int opensAt, int closesAt, Restaurant restaurant)
        {
            DayOfWeek = dayOfWeek;
            OpensAt = opensAt;
            ClosesAt = closesAt;
            RestaurantId = restaurant.Id;
            Restaurant = restaurant;
        }

        protected DayOfWork()
        {

        }
    }
}
