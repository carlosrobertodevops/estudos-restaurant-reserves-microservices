namespace Security.API.IdentityManager
{
    public interface IIdentityManager
    {
        Task<AccessTokenViewModel> LoginAsync(string username, string password, Guid correlationId, CancellationToken cancellationToken);
        Task<AccessTokenViewModel> CreateAccountAsync(string username, string password, string firstName, string lastName, Guid correlationId, CancellationToken cancellationToken);
    }
}
