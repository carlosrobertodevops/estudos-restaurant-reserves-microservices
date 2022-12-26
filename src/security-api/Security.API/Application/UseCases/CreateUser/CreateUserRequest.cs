namespace Security.API.UseCases.CreateUser
{
    public class CreateUserRequest : IUseCase<CreateRestaurantEventResponse>
    {
        public UserViewModel User { get; set; }
        public Guid CorrelationId { get; set; }

        public CreateUserRequest(UserViewModel user, Guid correlationId)
        {
            User = user;
            CorrelationId = correlationId;
        }
    }
}
