namespace Restaurant.Application.RequestValidators
{
    public class GetRestaurantByIdQueryValidator : AbstractValidator<GetRestaurantByIdQuery>
    {
        public GetRestaurantByIdQueryValidator()
        {
            RuleFor(r => r.Id).NotEmpty().NotNull().NotEqual(Guid.NewGuid());
        }
    }
}
