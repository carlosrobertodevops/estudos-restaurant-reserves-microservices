namespace Restaurant.Application.RequestValidators
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);

            RuleFor(r => r.Restaurant).NotNull();
        }
    }
}
