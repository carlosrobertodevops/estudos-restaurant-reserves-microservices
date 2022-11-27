namespace Restaurant.Application.RequestValidators
{
    public class DeleteRestaurantCommandValidator : AbstractValidator<DeleteRestaurantCommand>
    {
        public DeleteRestaurantCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
