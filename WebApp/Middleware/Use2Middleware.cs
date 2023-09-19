namespace WebApp.Middleware
{
    public class Use2Middleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Begin of Use2");

            await next(context);

            Console.WriteLine("End of Use2");
        }
    }
}
