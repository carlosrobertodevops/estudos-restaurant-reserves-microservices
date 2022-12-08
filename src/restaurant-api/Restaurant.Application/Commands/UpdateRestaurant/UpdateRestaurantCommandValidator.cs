namespace Restaurant.Application.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(x => x.Id).NotNull()
                              .WithMessage(r => $"'{nameof(r.Id)}' must not be null.")
                              .NotEmpty()
                              .NotEqual(Guid.Empty);

            RuleFor(r => r.Restaurant).NotNull();
        }
    }
}
