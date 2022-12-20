namespace WebBff.API.UseCases.Restaurants.Delete
{
    public class DeleteRestaurant : IUseCase
    {
        public DeleteRestaurant(Guid id, Guid correlationId)
        {
            Id = id;
            CorrelationId = correlationId;
        }

        public Guid Id { get; }
        public Guid CorrelationId { get; }
    }
}
