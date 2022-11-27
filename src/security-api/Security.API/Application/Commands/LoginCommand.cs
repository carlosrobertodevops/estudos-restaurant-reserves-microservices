namespace Security.API.Application.Commands
{
    public class LoginCommand
    {
        public static string Route => "/api/security/login";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        internal static Task<IResult> Action()
        {
            return Task.FromResult(Results.Ok());
        }
    }
}
