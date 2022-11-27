namespace Security.API.Application.Commands
{
    public class LogoutCommand
    {
        public static string Route => "/api/security/logout";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        internal static Task<IResult> Action()
        {
            return Task.FromResult(Results.Ok());
        }
    }
}
