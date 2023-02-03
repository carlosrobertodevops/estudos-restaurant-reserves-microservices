namespace Security.API.Application.UseCases.DeleteUser
{
    public class DeleteUserRequest : IUseCase
    {
        public Guid Id { get; private set; }
        public Guid CorrelationId { get; private set; }

        public DeleteUserRequest(Guid id, Guid correlationId)
        {
            Id = id;
            CorrelationId = correlationId;
        }
    }
}
