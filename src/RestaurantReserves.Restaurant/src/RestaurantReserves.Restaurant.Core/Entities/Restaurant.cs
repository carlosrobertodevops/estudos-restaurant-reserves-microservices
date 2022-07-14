namespace RestaurantReserves.Restaurant.Core.Entities
{
    public class Restaurant
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsOpen { get; private set; }
        public bool Enabled { get; private set; }
        public bool IsVerified { get; private set; }
        public string Cnpj { get; private set; }
        public string Email { get; private set; }
        public Phone Phone { get; private set; }
        public Address Address { get; private set; }
        public ICollection<AcceptedPaymentMethod> AcceptedPaymentMethods { get; private set; }
        public VerificationProccessStatus VerificationProccessStatus { get; private set; }
        public ICollection<Schedule> Schedules { get; private set; }

        public Category Category { get; private set; }
        public Guid CategoryId { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Restaurant(string name, string cnpj, string email)
        {
            Name = name;
            Cnpj = new Cnpj(cnpj).Number;
            Email = new Email(email).Address;

            Address = new Address();
            IsOpen = false;
            IsVerified = false;
            Enabled = true;
            VerificationProccessStatus = VerificationProccessStatus.NotStarted;

            Validate();
        }

        protected Restaurant() { }

        public Restaurant(ObjectMarker marker)
        {
            if (marker != ObjectMarker.INVALID)
                throw new ObjectWithoutParamCreationAtemptException();

            Id = Guid.Empty;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new BusinessException("Invalid name.");

            if(Name.Length < 5)
                throw new BusinessException("The Name field needs to have at least 5 chars.");
        }

        public bool Exists()
        {
            return Id != Guid.Empty;
        }

        private void SetCategory(Category category)
        {
            if (category == null || !category.Exists())
                return;

            CategoryId = category.Id;
        }

        public void SetProperties(string areaCode,
                                  string number,
                                  string description,
                                  ICollection<AcceptedPaymentMethod> acceptedPaymentMethod,
                                  Category category,
                                  Address address,
                                  ICollection<Schedule> schedules)
        {
            ChangePhone(areaCode, number);

            ChangeDescription(description);

            SetAcceptedPaymentMethods(acceptedPaymentMethod);

            SetCategory(category);

            ChangeAddress(address);

            SetSchedules(schedules);
        }

        public void Update(string name,
                           string email,
                           string areaCode,
                           string number,
                           string description,
                           Category category,
                           Address address)
        {
            ChangeName(name);

            ChangeEmail(email);

            ChangePhone(areaCode, number);

            ChangeDescription(description);

            ChangeCategory(category);

            ChangeAddress(address);

            //Pblish event
        }

        private void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (name.Length < 5)
                throw new BusinessException("The Name field needs to have at least 5 chars.");

            Name = name;
        }

        private void ChangeEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return;

            Email = new Email(email).Address;
        }

        private void ChangePhone(string areaCode, string number)
        {
            if (string.IsNullOrEmpty(areaCode) && string.IsNullOrEmpty(number))
                return;

            Phone = new Phone(areaCode, number);
        }

        private void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                return;

            if (Name.Length < 5)
                throw new BusinessException("The Description field needs to have at least 5 chars.");

            Description = description;
        }

        public void SetAcceptedPaymentMethods(ICollection<AcceptedPaymentMethod> acceptedPaymentMethod)
        {
            if (!acceptedPaymentMethod.Any())
                return;

            AcceptedPaymentMethods = acceptedPaymentMethod.Select(a => new AcceptedPaymentMethod(a.Name, a.Enabled)).ToList();
        }

        public void ChangeCategory(Category category)
        {
            if (category == null || !category.Exists())
                return;

            Category = category;

            CategoryId = category.Id;
        }

        private void ChangeAddress(Address address)
        {
            if (address == null)
                return;

            Address = address;
        }

        private void SetSchedules(ICollection<Schedule> schedules)
        {
            if (schedules == null)
                return;

            Schedules = schedules.Select(s => new Schedule(s.Day, s.OpensAt, s.ClosesAt, s.Enabled))
                                 .ToList();
        }

        public void Open()
        {
            if (!Enabled) throw new BusinessException("The restaurant is disabled.");

            if (IsOpen) throw new BusinessException("The restaurant is already open.");

            IsOpen = true;
        }

        public void Close()
        {
            if (!Enabled) throw new BusinessException("The restaurant is disabled.");

            if (!IsOpen) throw new BusinessException("The restaurant is already closed.");

            IsOpen = false;
        }

        public void DisableAccount()
        {
            if (!Enabled) throw new BusinessException("The restaurant is already disabled.");

            Enabled = false;
        }

        public void EnableAccount()
        {
            if(!Enabled) throw new BusinessException("The restaurant is already enabled.");

            Enabled = true;
        }

        public void StartVerificationProccess()
        {
            if (!Enabled) throw new BusinessException("The restaurant is disabled.");

            if (VerificationProccessStatus == VerificationProccessStatus.Finished) 
                throw new BusinessException("O restaurante já é verificado.");

            if (VerificationProccessStatus == VerificationProccessStatus.Started) 
                throw new BusinessException("O restaurante já está em processo de verificação.");

            VerificationProccessStatus = VerificationProccessStatus.Started;
        }

        public void CancelVerificationProccess()
        {
            if (!Enabled) throw new BusinessException("The restaurant is disabled.");

            VerificationProccessStatus = VerificationProccessStatus.NotStarted;

            IsVerified = false;
        }

        public void AddVerifiedFlag()
        {
            if (!Enabled) throw new BusinessException("The restaurant is disabled.");

            VerificationProccessStatus = VerificationProccessStatus.Finished;

            IsVerified = true;
        }
    }
}
