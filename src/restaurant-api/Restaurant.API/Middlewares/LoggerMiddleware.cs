using Restaurant.API.Extensions;

namespace Restaurant.API.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;
        private readonly IDateTimeProvider _dateTime;
        private Guid _correlationId;

        public LoggerMiddleware(RequestDelegate next, 
                                ILogger<LoggerMiddleware> logger,
                                IDateTimeProvider dateTime)
        {
            _next = next;
            _logger = logger;
            _dateTime = dateTime;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await HandleLogsAsync(context);

            await _next(context);
        }

        private Task HandleLogsAsync(HttpContext context)
        {
            _correlationId = context.Request.GetCorrelationId();

            if (context.User is null || !context.User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("Anonymous request to route", new {context, logDate = _dateTime.Now, correlationId = _correlationId });

                return Task.CompletedTask;
            }

            var userId = context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation("Authenticated request to route", new { userId, context, logDate = _dateTime.Now, correlationId = _correlationId });

            return Task.CompletedTask;
        }
    } 
}
