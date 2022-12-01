namespace Restaurant.Application.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandValidator : AbstractValidator<DeleteRestaurantCommand>
    {
        public DeleteRestaurantCommandValidator()
        {
            RuleFor(x => x.Id).NotNull()
                               .WithMessage(r => $"'{nameof(r.Id)}' must not be null.")
                               .NotEmpty()
                               .NotEqual(Guid.Empty);
        }
    }
}
