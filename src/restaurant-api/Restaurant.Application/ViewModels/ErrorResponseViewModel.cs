namespace Restaurant.Application.ViewModels
{
    public class ErrorResponseViewModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("errors")]
        public IDictionary<string, string[]> Errors { get; set; }
        [JsonProperty("correlationId")]
        public Guid CorrelationId { get; set; }

        public ErrorResponseViewModel(Exception exception, Guid correlationId)
        {
            Message = exception.Message;
            Errors = new Dictionary<string, string[]>();
            CorrelationId = correlationId;
        }

        public ErrorResponseViewModel(BusinessException exception)
        {
            Message = exception.Message;
            Errors = exception.ValidationErrors;
            CorrelationId = exception.CorrelationId;
        }
    }
}
