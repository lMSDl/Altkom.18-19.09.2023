using WebApp.Services;

namespace WebApp.Middleware
{
    public class CounterMiddleware : IMiddleware
    {
        private readonly CounterService _counterService;

        public CounterMiddleware(CounterService counterService)
        {
            _counterService = counterService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _counterService.RaiseCounter();

            await next(context);
        }
    }
}
