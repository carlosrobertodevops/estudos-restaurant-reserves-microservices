namespace Restaurant.Application.RequestValidators
{
    public class GetRestaurantsByAddressQueryValidator : AbstractValidator<GetRestaurantsByAddressQuery>
    {
        public GetRestaurantsByAddressQueryValidator()
        {
            RuleFor(r => r.Page).NotNull().GreaterThan(0);

            RuleFor(r => r.Rows).NotNull().GreaterThan(0);

            RuleFor(r => r.Neighborhood).NotNull().NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.City) && string.IsNullOrWhiteSpace(x.Zone));
           
            RuleFor(r => r.City).NotNull().NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.Neighborhood) && string.IsNullOrWhiteSpace(x.Zone));
            
            RuleFor(r => r.Zone).NotNull().NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.City) && string.IsNullOrWhiteSpace(x.Neighborhood));
        }
    }
}
