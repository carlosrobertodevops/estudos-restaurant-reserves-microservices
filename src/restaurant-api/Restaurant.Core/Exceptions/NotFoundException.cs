namespace Restaurant.Core.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message = "Not found") : base(message)
        {
        }
        public NotFoundException(Guid correlationId, string message = "Not found") : base(message, correlationId)
        {
        }
    }
}
