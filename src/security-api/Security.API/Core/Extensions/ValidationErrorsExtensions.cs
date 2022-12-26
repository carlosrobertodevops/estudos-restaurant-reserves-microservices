namespace Security.API.Extensions
{
    public static class ValidationErrorsExtensions
    {
        public static IDictionary<string, string[]> AsValidationErrors(this KeycloakErrorViewModel keycloakError)
        {
            return new Dictionary<string, string[]>
            {
                {keycloakError.Error, new string[1]{keycloakError.ErrorDescription} }
            };
        }
    }
}
