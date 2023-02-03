namespace Security.API.Application.UseCases.UpdateUser
{
    public sealed class UpdateUserRequest : IUseCase
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public Guid CorrelationId { get; private set; }

        public UpdateUserRequest(Guid id, 
                                string firstName, 
                                string lastName, 
                                string username, 
                                string password, 
                                Guid correlationId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            CorrelationId = correlationId;
        }

        public UpdateUserRequest(Guid id,
                                string firstName,
                                string lastName,
                                Guid correlationId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = string.Empty;
            Password = string.Empty;
            CorrelationId = correlationId;
        }
    }
}
