namespace Restaurant.Application.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(r => r.Restaurant).NotNull();
        }
    }
}
