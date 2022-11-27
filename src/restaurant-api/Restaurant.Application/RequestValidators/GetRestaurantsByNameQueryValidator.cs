namespace Restaurant.Application.RequestValidators
{
    public class GetRestaurantsByNameQueryValidator : AbstractValidator<GetRestaurantsByNameQuery>
    {
        public GetRestaurantsByNameQueryValidator()
        {
            RuleFor(r => r.Page).NotNull().GreaterThan(0);

            RuleFor(r => r.Rows).NotNull().GreaterThan(0);

            RuleFor(r => r.Name).NotNull().NotEmpty();
        }
    }
}
