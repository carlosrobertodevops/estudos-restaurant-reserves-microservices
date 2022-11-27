namespace Security.API.Application.Commands
{
    public class UpdateAccountCommand
    {
        public static string Route => "/api/security/update-account";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        internal static Task<IResult> Action()
        {
            return Task.FromResult(Results.Ok());
        }
    }
}
