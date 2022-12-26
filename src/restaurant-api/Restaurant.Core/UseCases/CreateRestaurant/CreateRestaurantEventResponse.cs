using EasyNetQ;

namespace EventBusMessages
{
    public class CreateRestaurantEventResponse : ResponseMessage
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public int RefreshExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int NotBeforePolicy { get; set; }
        public string SessionState { get; set; }
        public string Scope { get; set; }

        public CreateRestaurantEventResponse(ValidationResult validationResult,
                                             Guid correlationId) : base(validationResult, correlationId)
        {

        }

        public CreateRestaurantEventResponse() : base(new ValidationResult(), Guid.NewGuid())
        {

        }
    }
}
