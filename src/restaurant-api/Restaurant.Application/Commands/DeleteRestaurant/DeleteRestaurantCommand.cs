namespace Restaurant.Application.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteRestaurantCommand(Guid id)
        {
            Id = id;
        }
    }
}
