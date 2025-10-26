    using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace TechnicalTask.BLL.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _limit;
        private readonly TimeSpan _period;
        private static readonly ConcurrentDictionary<string, (int Count, DateTime Timestamp)> _requests = new();

        public RateLimitingMiddleware(RequestDelegate next, int limit, TimeSpan period)
        {
            _next = next;
            _limit = limit;
            _period = period;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;

            var entry = _requests.GetOrAdd(key, _ => (0, now));

            if ((now - entry.Timestamp) > _period)
            {
                entry = (0, now);
            }

            if (entry.Count >= _limit)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too Many Requests");
                return;
            }

            _requests[key] = (entry.Count + 1, entry.Timestamp);
            await _next(context);
        }
    }
}
