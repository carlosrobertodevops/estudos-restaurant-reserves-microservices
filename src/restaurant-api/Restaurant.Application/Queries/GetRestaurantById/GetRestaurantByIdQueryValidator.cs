namespace Restaurant.Application.Queries.GetRestaurantById
{
    public sealed class GetRestaurantByIdQueryValidator : AbstractValidator<GetRestaurantByIdQuery>
    {
        public GetRestaurantByIdQueryValidator()
        {
            RuleFor(r => r.Id).NotEmpty()
                              .NotNull()
                              .WithMessage(r => $"'{nameof(r.Id)}' must not be null.")
                              .NotEqual(Guid.NewGuid());
        }
    }
}
