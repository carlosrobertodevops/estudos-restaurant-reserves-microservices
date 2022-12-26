using RestaurantEntity = Restaurant.Core.Entities.Restaurant;

namespace Restaurant.Core.Validator
{
    public class RestaurantValidator : AbstractValidator<RestaurantEntity>
    {
        public RestaurantValidator()
        {
            RuleFor(r => r.Name).NotNull()
                                .NotEmpty()
                                .MinimumLength(5)
                                .MaximumLength(200);

            RuleFor(r => r.Document).NotEmpty()
                                    .NotNull()
                                    .SetValidator(new DocumentValidator<RestaurantEntity, string>());

            RuleFor(r => r.User).NotEmpty()
                                .NotNull();

            RuleFor(r => r.User.FirstName).NotEmpty()
                                          .NotNull();

            RuleFor(r => r.User.LastName).NotEmpty()
                                         .NotNull();
        }
    }
}
