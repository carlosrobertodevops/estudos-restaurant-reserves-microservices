namespace Restaurant.Core.Entities
{
    public sealed class DayOfWork : BaseEntity
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

            Validate();
        }

        protected DayOfWork()
        {

        }

        private void Validate()
        {
            if (!this.IsValid())
            {
                throw new BusinessException(GenerateValidationErrors(), "Invalid day of work");
            }
        }

        private IDictionary<string, string[]> GenerateValidationErrors()
        {
            var errors = new Dictionary<string, string[]>();

            if (InvalidOpensAt())
            {
                errors.Add("'Opens at'", new string[2]
                {
                    "'Opens at' must be in the range of '0' to '24'", 
                    "'Opens at' must be lower than 'Closes at'"
                });
            }

            if (InvalidClosesAt())
            {
                errors.Add("'Closes at'", new string[2]
                {
                    "'Closes at' must be in the range of '0' to '24'",
                    "'Closes at' must be higher than 'Opens at'"
                });
            }

            return errors;
        }

        private bool InvalidOpensAt()
        {
            return OpensAt > 24 ||
                   OpensAt < 0 ||
                   OpensAt > ClosesAt;
        }

        private bool InvalidClosesAt()
        {
            return ClosesAt > 24 &&
                   ClosesAt < 0 &&
                   ClosesAt < OpensAt;
        }
    }
}
