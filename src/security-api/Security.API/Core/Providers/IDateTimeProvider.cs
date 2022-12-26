namespace Security.API.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
