namespace WebApp.Middleware
{
    public class Use1Middleware
    {
        private readonly RequestDelegate _next;

        public Use1Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Begin of Use1");

            await _next(context);

            Console.WriteLine("End of Use1");
        }
    }
}
