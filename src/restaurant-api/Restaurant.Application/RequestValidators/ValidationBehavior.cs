namespace Restaurant.Application.RequestValidators
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationErrors = _validators.Select(validation => validation.Validate(context))
                                              .Where(validation => validation is not null && !validation.IsValid)
                                              .SelectMany(validationResult => validationResult.ToDictionary())
                                              .ToDictionary(vr => vr.Key, vr => vr.Value);

            if(validationErrors.Any())
            {
                throw new BusinessException(validationErrors, "Invalid request");
            }

            return next();
        }
    }
}
