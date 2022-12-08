namespace Restaurant.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public IDictionary<string, string[]> ValidationErrors { get; private set; }
        public Guid CorrelationId { get; private set; }

        public BusinessException(string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string[]>();
            CorrelationId = Guid.NewGuid();
        }

        public BusinessException(string message, Guid correlationId) : base(message)
        {
            ValidationErrors = new Dictionary<string, string[]>();
            CorrelationId = correlationId;
        }

        public BusinessException(IDictionary<string, string[]> validationErrors, string message)
            : base(message)
        {
            ValidationErrors = validationErrors;
            CorrelationId = Guid.NewGuid();
        }
    }
}
