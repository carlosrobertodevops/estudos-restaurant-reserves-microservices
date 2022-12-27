namespace Restaurant.Application.Commands.CreateRestaurant
{
    public sealed class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(r => r.Restaurant).NotNull();
        }
    }
}
