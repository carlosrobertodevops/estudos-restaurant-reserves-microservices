using FluentValidation;

namespace Security.API.Core.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(u => u.FirstName).NotNull().NotEmpty();
            RuleFor(u => u.LastName).NotNull().NotEmpty();
            RuleFor(u => u.Username).NotNull().NotEmpty().MinimumLength(5).MaximumLength(18);
            RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(7).MaximumLength(18);
        }
    }
}
