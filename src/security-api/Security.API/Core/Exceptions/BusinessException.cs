namespace Security.API.Exceptions
{
    public class BusinessException : Exception
    {
        public IDictionary<string, string[]> ValidationErrors { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Guid CorrelationId { get; private set; }

        public BusinessException(IDictionary<string, string[]> validationErrors, string message, HttpStatusCode statusCode, Guid correlationId)
            : base(message)
        {
            ValidationErrors = validationErrors;
            StatusCode = statusCode;
            CorrelationId = correlationId;
        }
    }
}
