namespace Restaurant.Application.Commands.DeleteRestaurant
{
    public sealed class DeleteRestaurantCommand : IDeleteRestaurantCommand
    {
        public Guid Id { get; private set; }
        public Guid CorrelationId { get; private set; }

        public DeleteRestaurantCommand(Guid id, Guid correlatioId)
        {
            Id = id;
            CorrelationId = correlatioId;
        }
    }
}
