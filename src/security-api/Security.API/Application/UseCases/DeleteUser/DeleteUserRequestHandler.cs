namespace Security.API.Application.UseCases.DeleteUser
{
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest>
    {
        public Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
