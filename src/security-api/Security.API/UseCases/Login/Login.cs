namespace Security.API.UseCases.Login
{
    public class Login : IUseCase<AccessTokenViewModel>
    {
        public UserViewModel User { get; set; }
        public Guid CorrelationId { get; private set; }

        public Login(UserViewModel user, Guid correlationId)
        {
            User = user;
            CorrelationId = correlationId;
        }
    }
}
