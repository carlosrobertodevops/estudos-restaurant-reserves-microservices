namespace Security.API.UseCases.Login
{
    public class LoginRequest : IUseCase<AccessTokenViewModel>
    {
        public UserViewModel User { get; set; }
        public Guid CorrelationId { get; private set; }

        public LoginRequest(UserViewModel user, Guid correlationId)
        {
            User = user;
            CorrelationId = correlationId;
        }
    }
}
