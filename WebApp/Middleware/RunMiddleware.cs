namespace WebApp.Middleware
{
    public class RunMiddleware
    {
        public RunMiddleware(RequestDelegate _)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {

            Console.WriteLine("Begin of Run");
            await context.Response.WriteAsync("Under Construction");
            Console.WriteLine("End of Run");
        }
    }
}
