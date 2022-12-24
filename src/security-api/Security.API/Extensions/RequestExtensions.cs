namespace Security.API.Extensions
{
    public static class RequestExtensions
    {
        public static string CorrelationId => "x-correlation-id";

        public static Guid GetCorrelationId(this HttpRequest request)
        {
            if (request?.Headers[CorrelationId] is null || !Guid.TryParse(request.Headers[CorrelationId], out var correlationId))
            {
                return Guid.Empty;
            }

            return correlationId;
        }
    }
}
