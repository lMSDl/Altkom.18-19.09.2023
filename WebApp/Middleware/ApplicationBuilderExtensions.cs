namespace WebApp.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Use1(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Use1Middleware>();
        }

        public static IApplicationBuilder HelloRun(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HelloRunMiddleware>();
        }

    }
}
