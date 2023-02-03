namespace Security.API.Core.ExternalServices
{
    public interface IIdentityManager
    {
        Task<AccessTokenViewModel> LoginAsync(string username, string password, Guid correlationId, CancellationToken cancellationToken);
        Task<AccessTokenViewModel> CreateAccountAsync(UserRepresentation user, string accessToken, Guid correlationId, CancellationToken cancellationToken);
    }
}
