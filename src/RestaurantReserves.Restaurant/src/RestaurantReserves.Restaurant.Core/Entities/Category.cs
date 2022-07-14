namespace RestaurantReserves.Restaurant.Core.Entities
{
    public class Category
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public int Code { get; private set; }
        public bool Enabled { get; private set; }

        public Category() { }
        public ICollection<Restaurant> Restaurants { get; set; }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;
            Enabled = true;

            Validate();
        }

        public Category(ObjectMarker objectMarker)
        {
            if (objectMarker != ObjectMarker.INVALID)
                throw new ObjectWithoutParamCreationAtemptException();

            Id = Guid.Empty;
        }

        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
                throw new BusinessException("Invalid name");

            if (Code.Equals(0))
                throw new BusinessException("The Code can't be 0");
        }

        public bool Exists()
        {
            return Id != Guid.Empty;
        }

        public void Disable()
        {
            if (!Enabled) throw new BusinessException("A categoria já está desabilitada.");

            Enabled = false;
        }

        public void Enable()
        {
            if (Enabled) throw new BusinessException("A categoria já está habilitada.");

            Enabled = true;
        }
    }
}
