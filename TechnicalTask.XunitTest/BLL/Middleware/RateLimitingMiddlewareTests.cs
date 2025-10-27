using System.Net;
using Microsoft.AspNetCore.Http;
using TechnicalTask.BLL.Middleware;

namespace TechnicalTask.XunitTest.BLL.Middleware
{
    public class RateLimitingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_RequestsWithinLimit_ShouldPass()
        {
            var nextCalled = false;
            RequestDelegate next = (ctx) =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new RateLimitingMiddleware(next, limit: 3, period: TimeSpan.FromMinutes(1));
            var context = CreateHttpContext("192.168.1.1");

            await middleware.InvokeAsync(context);

            Assert.True(nextCalled);
            Assert.NotEqual(StatusCodes.Status429TooManyRequests, context.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_ExceedingLimit_ShouldReturn429()
        {
            RequestDelegate next = (ctx) => Task.CompletedTask;
            var middleware = new RateLimitingMiddleware(next, limit: 2, period: TimeSpan.FromMinutes(1));
            var ipAddress = "192.168.1.1";

            var context1 = CreateHttpContext(ipAddress);
            await middleware.InvokeAsync(context1);

            var context2 = CreateHttpContext(ipAddress);
            await middleware.InvokeAsync(context2);

            var context3 = CreateHttpContext(ipAddress);
            await middleware.InvokeAsync(context3);

            Assert.Equal(StatusCodes.Status429TooManyRequests, context3.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_AfterPeriodExpires_ShouldResetLimit()
        {
            RequestDelegate next = (ctx) => Task.CompletedTask;
            var middleware = new RateLimitingMiddleware(next, limit: 1, period: TimeSpan.FromMilliseconds(100));
            var ipAddress = "192.168.1.1";

            var context1 = CreateHttpContext(ipAddress);
            await middleware.InvokeAsync(context1);

            var context2 = CreateHttpContext(ipAddress);
            await middleware.InvokeAsync(context2);
            Assert.Equal(StatusCodes.Status429TooManyRequests, context2.Response.StatusCode);

            await Task.Delay(150);

            var context3 = CreateHttpContext(ipAddress);
            await middleware.InvokeAsync(context3);

            Assert.NotEqual(StatusCodes.Status429TooManyRequests, context3.Response.StatusCode);
        }

        [Fact]
        public async Task InvokeAsync_NullIPAddress_ShouldUseUnknownKey()
        {
            // Arrange
            var nextCalled = false;
            RequestDelegate next = (ctx) =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new RateLimitingMiddleware(next, limit: 1, period: TimeSpan.FromMinutes(1));
            var context = CreateHttpContext(null);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
        }
        private HttpContext CreateHttpContext(string? ipAddress)
        {
            var context = new DefaultHttpContext();

            if (ipAddress != null)
            {
                context.Connection.RemoteIpAddress = IPAddress.Parse(ipAddress);
            }

            return context;
        }
    }

}
