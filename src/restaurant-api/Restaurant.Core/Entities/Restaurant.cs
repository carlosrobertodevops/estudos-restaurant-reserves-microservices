namespace Restaurant.Core.Entities
{
    public sealed class Restaurant : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Address Address { get; private set; }
        public int TotalTables { get; private set; }
        public bool Enabled { get; private set; }
        public ICollection<DayOfWork> DaysOfWork { get; private set; }
        public ICollection<Contact> Contacts { get; private set; }

        public Restaurant(string name, string description, Address address, int? totalTables)
        {
            Name = name;
            Description = description;
            Address = address;
            TotalTables = totalTables ?? 0;
            Enabled = true;

            Validate();
        }

        private void Validate()
        {

        }

        public Restaurant()
        {
            Id = Guid.Empty;
        }

        public bool IsValid()
        {
            return Id != Guid.Empty;
        }

        public void Update(string name,
                           string description,
                           Address address,
                           int? totalTable,
                           bool? enable,
                           ICollection<DayOfWork> daysOfWork)
        {
            UpdateName(name);

            UpdateDescription(description);

            UpdateAddress(address);

            UpdateTotalTables(totalTable);

            UpdateEnabled(enable);

            UpdateDaysOfWork(daysOfWork);
        }

        private void UpdateName(string name)
        {
            if (name is null || name == Name)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length < 5 || name.Length > 200)
            {
                throw new BusinessException("Invalid name");
            }

            Name = name;
        }

        private void UpdateDescription(string description)
        {
            if (description is null)
            {
                return;
            }

            Description = description;
        }

        private void UpdateAddress(Address address)
        {
            if (address is null)
            {
                return;
            }

            Address = address;
        }

        private void UpdateTotalTables(int? quantity)
        {
            if (quantity is null)
            {
                return;
            }

            TotalTables = quantity.Value;
        }

        private void UpdateEnabled(bool? enable)
        {
            if (enable is null)
            {
                return;
            }

            if (enable.Value)
            {
                Enable();

                return;
            }

            Disable();
        }

        public void Enable()
        {
            if (Enabled)
            {
                throw new BusinessException("The restaurant is already enabled");
            }

            Enabled = true;
        }

        public void Disable()
        {
            if (!Enabled)
            {
                throw new BusinessException("The restaurant is already disabled");
            }

            Enabled = false;
        }

        private void UpdateDaysOfWork(ICollection<DayOfWork> daysOfWork)
        {
            if (daysOfWork is null)
            {
                return;
            }

            DaysOfWork = daysOfWork;
        }

        private void UpdateContacts(ICollection<Contact> contacts)
        {
            if (contacts is null)
            {
                return;
            }

            Contacts = contacts;
        }
    }
}
