using FluentValidation.Results;
using Security.API.Core.Events;

namespace EventBusMessages
{
    public class CreateUserEventResponse : ResponseMessage
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public int RefreshExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int NotBeforePolicy { get; set; }
        public string SessionState { get; set; }
        public string Scope { get; set; }

        public CreateUserEventResponse(AccessTokenViewModel accessToken,
                                       ValidationResult validationResult,
                                       Guid correlationId) : base(validationResult, correlationId)
        {
            AccessToken = accessToken.AccessToken;
            ExpiresIn = accessToken.ExpiresIn;
            RefreshExpiresIn = accessToken.RefreshExpiresIn;
            RefreshToken = accessToken.RefreshToken;
            TokenType = accessToken.TokenType;
            NotBeforePolicy = accessToken.NotBeforePolicy;
            SessionState = accessToken.SessionState;
            Scope = accessToken.Scope;
        }


        public CreateUserEventResponse(ValidationResult validationResult,
                                       Guid correlationId) : base(validationResult, correlationId)
        {

        }

        public CreateUserEventResponse() : base(new ValidationResult(), Guid.NewGuid())
        {

        }
    }
}
