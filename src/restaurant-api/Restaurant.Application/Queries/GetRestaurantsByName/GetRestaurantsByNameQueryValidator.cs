namespace Restaurant.Application.Queries.GetRestaurantsByName
{
    public sealed class GetRestaurantsByNameQueryValidator : AbstractValidator<GetRestaurantsByNameQuery>
    {
        public GetRestaurantsByNameQueryValidator()
        {
            RuleFor(r => r.Page).NotNull()
                                .WithMessage(r => $"'{nameof(r.Page)}' must not be null.")
                                .GreaterThan(0);

            RuleFor(r => r.Rows).NotNull()
                                .WithMessage(r => $"'{nameof(r.Rows)}' must not be null.")
                                .GreaterThan(0);

            RuleFor(r => r.Name).NotNull()
                                .WithMessage(r => $"'{nameof(r.Name)}' must not be null.")
                                .NotEmpty();
        }
    }
}
