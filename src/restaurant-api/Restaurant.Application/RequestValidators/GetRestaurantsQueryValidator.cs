namespace Restaurant.Application.RequestValidators
{
    public class GetRestaurantsQueryValidator : AbstractValidator<GetRestaurantsQuery>
    {
        public GetRestaurantsQueryValidator()
        {
            RuleFor(r => r.Page).NotNull()
                                .WithMessage(r => $"'{nameof(r.Page)}' must not be null.")
                                .GreaterThan(0);

            RuleFor(r => r.Rows).NotNull()
                                .WithMessage(r => $"'{nameof(r.Rows)}' must not be null.")
                                .GreaterThan(0);
        }
    }
}
