namespace Restaurant.Application.Queries.GetRestaurantsByAddress
{
    public sealed class GetRestaurantsByAddressQueryValidator : AbstractValidator<GetRestaurantsByAddressQuery>
    {
        public GetRestaurantsByAddressQueryValidator()
        {
            RuleFor(r => r.Page).NotNull().GreaterThan(0);

            RuleFor(r => r.Rows).NotNull().GreaterThan(0);

            RuleFor(r => r.Neighborhood).NotNull()
                                        .Unless(x => !string.IsNullOrWhiteSpace(x.City) || !string.IsNullOrWhiteSpace(x.Zone))
                                        .WithMessage(r => $"'{nameof(r.Neighborhood)}' must not be null.")
                                        .NotEmpty()
                                        .Unless(x => !string.IsNullOrWhiteSpace(x.City) || !string.IsNullOrWhiteSpace(x.Zone));

            RuleFor(r => r.City).NotNull()
                                .Unless(x => !string.IsNullOrWhiteSpace(x.Neighborhood) || !string.IsNullOrWhiteSpace(x.Zone))
                                .WithMessage(r => $"'{nameof(r.City)}' must not be null.")
                                .NotEmpty()
                                .Unless(x => !string.IsNullOrWhiteSpace(x.Neighborhood) || !string.IsNullOrWhiteSpace(x.Zone));

            RuleFor(r => r.Zone).NotNull()
                                .Unless(x => !string.IsNullOrWhiteSpace(x.City) || !string.IsNullOrWhiteSpace(x.Neighborhood))
                                .WithMessage(r => $"'{nameof(r.Zone)}' must not be null.")
                                .NotEmpty()
                                .Unless(x => !string.IsNullOrWhiteSpace(x.City) || !string.IsNullOrWhiteSpace(x.Neighborhood));
        }
    }
}
