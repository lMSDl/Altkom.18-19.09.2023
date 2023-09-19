using WebApp.Services;

namespace WebApp.Middleware
{
    public class ReadCounterMiddleware
    {
        private readonly CounterService _counterService;
        public ReadCounterMiddleware(RequestDelegate _, CounterService counterService)
        {
            _counterService = counterService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"Counter: {_counterService.Counter}");
        }
    }
}
