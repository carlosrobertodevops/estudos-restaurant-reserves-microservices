namespace Security.API.Application.Commands
{
    public class DeleteAccountCommand
    {
        public static string Route => "/api/security/delete-account";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        internal static Task<IResult> Action()
        {
            return Task.FromResult(Results.Ok());
        }
    }
}
