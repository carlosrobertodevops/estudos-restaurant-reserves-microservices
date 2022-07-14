using RestaurantReserves.Restaurant.Core.DomainObjects;
using RestaurantReserves.Restaurant.Core.Exceptions;

namespace RestaurantReserves.Restaurant.Core.Entities
{
    public class AcceptedPaymentMethod
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        protected AcceptedPaymentMethod() { }

        public AcceptedPaymentMethod(string name, bool enabled, Restaurant restaurant)
        {
            Name = name;
            Enabled = enabled;
            RestaurantId = restaurant.Id;

            Validate();

            if (Guid.Empty.Equals(restaurant.Id))
                throw new BusinessException("Restaurant not found.");
        }

        public AcceptedPaymentMethod(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;

            Validate();
        }

        public AcceptedPaymentMethod(ObjectMarker marker)
        {
            if (marker != ObjectMarker.INVALID)
                throw new ObjectWithoutParamCreationAtemptException();

            Id = Guid.Empty;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new BusinessException("Invalid name");
        }

        public bool Exists()
        {
            return Id != Guid.Empty;
        }

        public void Update(string name, bool enabled)
        {
            ChangeName(name);

            ChangeEnabled(enabled);
        }

        private void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException("Invalid name");

            Name = name;
        }

        private void ChangeEnabled(bool enabled)
        {
            if (enabled) Enable();

            else Disable();
        }

        private void Enable()
        {
            if (Enabled) throw new BusinessException("The payment method is already enabled.");

            Enabled = true;
        }

        private void Disable()
        {
            if (!Enabled) throw new BusinessException("The payment method is already disabled.");

            Enabled = false;
        }
    }
}
