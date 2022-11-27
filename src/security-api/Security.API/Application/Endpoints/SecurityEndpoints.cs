namespace Security.API.Application.Endpoints
{
    public class SecurityEndpoints
    {
        public static WebApplication DefineEndpoints(WebApplication app)
        {
            app.MapMethods(CreateAccountCommand.Route, CreateAccountCommand.Methods, CreateAccountCommand.Handle)
              .WithTags("Security")
              .ProducesValidationProblem()
              .Produces(StatusCodes.Status200OK);

            app.MapMethods(UpdateAccountCommand.Route, UpdateAccountCommand.Methods, UpdateAccountCommand.Handle)
             .WithTags("Security")
             .ProducesValidationProblem()
             .Produces(StatusCodes.Status200OK);

            app.MapMethods(LoginCommand.Route, LoginCommand.Methods, LoginCommand.Handle)
             .WithTags("Security")
             .ProducesValidationProblem()
             .Produces(StatusCodes.Status200OK);

            app.MapMethods(LogoutCommand.Route, LogoutCommand.Methods, LogoutCommand.Handle)
             .WithTags("Security")
             .ProducesValidationProblem()
             .Produces(StatusCodes.Status200OK);

            app.MapMethods(DeleteAccountCommand.Route, DeleteAccountCommand.Methods, DeleteAccountCommand.Handle)
             .WithTags("Security")
             .ProducesValidationProblem()
             .Produces(StatusCodes.Status200OK);

            return app;
        }
    }
}
