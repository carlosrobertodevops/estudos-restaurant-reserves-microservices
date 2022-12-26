using FluentValidation.Results;

namespace Security.API.Core.Events
{
    public class ResponseMessage : Message
    {
        public ValidationResult ValidationResult { get; private set; }

        public ResponseMessage(ValidationResult validationResult, Guid correlationId) : base(correlationId)
        {
            ValidationResult = validationResult;
        }
    }
}
