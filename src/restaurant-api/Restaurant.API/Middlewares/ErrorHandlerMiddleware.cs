namespace Restaurant.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
                                      ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message, ex.CorrelationId);

                await HandleExceptionAsync(context, ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex.Message, ex.CorrelationId);

                await HandleExceptionAsync(context, ex);
            }
            catch (InfrastructureException ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, ex.CorrelationId);

                await HandleExceptionAsync(context, ex, ex.CorrelationId);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Something unexpected happened.", ex);

                await HandleExceptionAsync(context, ex, Guid.Empty);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, NotFoundException exception)
        {
            var code = HttpStatusCode.NotFound;

            return ErrorResponse(context, exception, code, exception.CorrelationId);
        }

        private static Task HandleExceptionAsync(HttpContext context, BusinessException exception)
        {
            var code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new ErrorResponseViewModel(exception));

            return ErrorResponse(context, result, code);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, Guid correlationId)
        {
            var code = HttpStatusCode.InternalServerError;

            return ErrorResponse(context, exception, code, correlationId);
        }

        private static Task ErrorResponse(HttpContext context, Exception exception, HttpStatusCode code, Guid correlationId)
        {
            var result = JsonConvert.SerializeObject(new ErrorResponseViewModel(exception, correlationId));

            return ErrorResponse(context, result, code);
        }

        private static Task ErrorResponse(HttpContext context, string result, HttpStatusCode code)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
