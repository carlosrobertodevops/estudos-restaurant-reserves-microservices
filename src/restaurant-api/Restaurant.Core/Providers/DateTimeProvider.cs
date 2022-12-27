namespace Restaurant.Core.Providers
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
