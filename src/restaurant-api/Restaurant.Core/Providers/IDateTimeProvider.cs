namespace Restaurant.Core.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
