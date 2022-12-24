namespace Security.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private Guid _correlationId;

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
                if (context.Request?.Headers?[RequestExtensions.CorrelationId] is null || !Guid.TryParse(context.Request?.Headers?[RequestExtensions.CorrelationId], out _correlationId))
                {
                    context.Request.Headers.Add(RequestExtensions.CorrelationId, Guid.NewGuid().ToString());

                    _correlationId = context.Request.GetCorrelationId();
                }

                await _next(context);
            }
            catch(BusinessException ex)
            {
                _logger.LogWarning(ex.Message, new { ex, correlationId = _correlationId });

                await HandleExceptionAsync(context, ex);
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Something unexpected happened.", new { ex, correlationId = _correlationId });

                await HandleExceptionAsync(context, ex, _correlationId);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, BusinessException exception)
        {
            var result = JsonConvert.SerializeObject(new ErrorResponseViewModel(exception));

            return ErrorResponse(context, result, exception.StatusCode);
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
