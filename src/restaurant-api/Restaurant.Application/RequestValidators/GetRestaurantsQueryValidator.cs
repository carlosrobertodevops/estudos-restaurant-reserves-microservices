namespace Restaurant.Application.RequestValidators
{
    public class GetRestaurantsQueryValidator : AbstractValidator<GetRestaurantsQuery>
    {
        public GetRestaurantsQueryValidator()
        {
            RuleFor(r => r.Page).NotNull().GreaterThan(0).LessThan(400);

            RuleFor(r => r.Rows).NotNull().GreaterThan(0);
        }
    }
}
