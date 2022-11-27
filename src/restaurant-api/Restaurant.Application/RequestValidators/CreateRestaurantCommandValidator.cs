namespace Restaurant.Application.RequestValidators
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(r => r.Restaurant).NotNull();
        }
    }
}
