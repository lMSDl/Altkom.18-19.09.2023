namespace WebApp.Middleware
{
    public class HelloRunMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate _)
        {

            Console.WriteLine("Begin of HelloRun");
            await context.Response.WriteAsync("Hello o/");
            Console.WriteLine("End of HelloRun");
        }
    }
}
