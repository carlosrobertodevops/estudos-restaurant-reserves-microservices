using RestaurantReserves.Restaurant.Core.DomainObjects;
using RestaurantReserves.Restaurant.Core.Exceptions;
using System.Text.RegularExpressions;

namespace RestaurantReserves.Restaurant.Core.Entities
{
    public class Schedule
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Day { get; set; }
        public string OpensAt { get; set; }
        public string ClosesAt { get; set; }
        public bool Enabled { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        protected Schedule() { }

        public Schedule(string day, string opensAt, string closesAt, bool enabled, Restaurant restaurant)
        {
            Day = day.ToUpper().Trim();
            OpensAt = opensAt;
            ClosesAt = closesAt;
            Enabled = enabled;
            RestaurantId = restaurant.Id;

            Validate();

            if (Guid.Empty.Equals(restaurant.Id))
                throw new BusinessException("Restaurant not found.");
        }

        public Schedule(string day, string opensAt, string closesAt, bool enabled)
        {
            Day = day;
            OpensAt = opensAt;
            ClosesAt = closesAt;
            Enabled = enabled;

            Validate();
        }

        public Schedule(ObjectMarker marker)
        {
            if (marker != ObjectMarker.INVALID)
                throw new ObjectWithoutParamCreationAtemptException();

            Id = Guid.Empty;
        }

        private void Validate()
        {
            if (!new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(OpensAt))
                throw new BusinessException("Invalid opens at format.");

            if (!new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(ClosesAt))
                throw new BusinessException("Invalid closes at format.");

            if (string.IsNullOrWhiteSpace(Day))
                throw new BusinessException("Invalid day.");
        }

        public bool Exists()
        {
            return Id != Guid.Empty;
        }

        public void Update(string day, string opensAt, string closesAt, bool enabled)
        {
            ChangeDay(day);

            ChangeOpensAt(opensAt);

            ChangeClosesAt(closesAt);

            ChangeEnabled(enabled);
        }

        private void ChangeDay(string day)
        {
            if (string.IsNullOrWhiteSpace(day))
                throw new BusinessException("Invalid day.");

            Day = day.ToUpper().Trim();
        }

        private void ChangeOpensAt(string opensAt)
        {
            if(!new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(opensAt))
                throw new BusinessException("Invalid opens at format.");

            OpensAt = opensAt;
        }

        private void ChangeClosesAt(string closesAt)
        {
            if (!new Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(closesAt))
                throw new BusinessException("Invalid closes at format.");

            ClosesAt = closesAt;
        }

        private void ChangeEnabled(bool enabled)
        {
            if (enabled) Enable();

            else Disable();
        }

        private void Enable()
        {
            if (Enabled) throw new BusinessException("The day is already enabled.");

            Enabled = true;
        }

        private void Disable()
        {
            if (!Enabled) throw new BusinessException("The day is already enabled.");

            Enabled = false;
        }
    }
}
