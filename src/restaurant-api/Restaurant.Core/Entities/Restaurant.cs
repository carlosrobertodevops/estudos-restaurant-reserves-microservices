namespace Restaurant.Core.Entities
{
    public sealed class Restaurant : BaseEntity
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Description { get; private set; }
        public Address Address { get; private set; }
        public int TotalTables { get; private set; }
        public bool Enabled { get; private set; }
        public User User { get; private set; }
        public ICollection<DayOfWork> DaysOfWork { get; private set; }
        public ICollection<Contact> Contacts { get; private set; }

        public Restaurant(string name, 
                          string document,
                          string description, 
                          User user,
                          Address address, 
                          int? totalTables, 
                          IValidator<Restaurant> validator,
                          Guid correlationId)
        {
            Name = name;
            Document = document is not null ? document.ParseCorrectFormat() : string.Empty;
            Description = description ?? string.Empty;
            User = user ?? new User();
            Address = address ?? new Address();
            TotalTables = totalTables ?? 0;
            Enabled = true;

            Validate(validator, correlationId);
        }

        private void Validate(IValidator<Restaurant> validator, Guid correlationId)
        {
            var validationResult = validator.Validate(this);

            if (validationResult.IsValid)
            {
                return;
            }

            throw new BusinessException(validationResult.ToDictionary(), "Invalid restaurant", correlationId);
        }

        public Restaurant()
        {
            Id = Guid.Empty;
        }

        public bool IsValid()
        {
            return Id != Guid.Empty;
        }

        public void Update(string name = null,
                           string document = null,
                           string description = null,
                           Address address = null,
                           int? totalTable = null,
                           bool? enable = null,
                           ICollection<DayOfWork> daysOfWork = null,
                           ICollection<Contact> contacts = null)
        {
            UpdateName(name);

            UpdateDocument(document);

            UpdateDescription(description);

            UpdateAddress(address);

            UpdateTotalTables(totalTable);

            UpdateEnabled(enable);

            UpdateDaysOfWork(daysOfWork);

            UpdateContacts(contacts);
        }

        private void UpdateName(string name)
        {
            if (name is null || name == Name)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length < 5 || name.Length > 200)
            {
                throw new BusinessException("Invalid name", Id);
            }

            Name = name;
        }

        private void UpdateDocument(string document)
        {
            if (document is null || document == Document)
            {
                return;
            }

            if(!DocumentValidatorHelper.IsValid(document))
            {
                throw new BusinessException("Invalid document", Id);
            }

            Document = document.ParseCorrectFormat();
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
                throw new BusinessException("The restaurant is already enabled", Id);
            }

            Enabled = true;
        }

        public void Disable()
        {
            if (!Enabled)
            {
                throw new BusinessException("The restaurant is already disabled", Id);
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
