using WebApp.Middleware;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

//konfiguracja service collection


//u¿ycie IMiddleware wymaga rejestracji 
builder.Services.AddTransient<Use2Middleware>();
builder.Services.AddTransient<HelloRunMiddleware>();
builder.Services.AddTransient<CounterMiddleware>();

builder.Services.AddSingleton<CounterService>();


var app = builder.Build();
//konfiguracja aplikacji

app.UseMiddleware<CounterMiddleware>();

app.Use1();
app.Map("/hello", HelloApp);
//u¿ycie IMiddleware wymaga rejestracji 
app.UseMiddleware<Use2Middleware>();


app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), nameApp =>
{
    nameApp.Run(async context =>
    {
        await context.Response.WriteAsync($"Witaj {context.Request.Query["name"]}");
    });

});

app.Map("/counter", counterApp => counterApp.UseMiddleware<ReadCounterMiddleware>());

app.UseMiddleware<RunMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => "Hello from development!");
}
else if (app.Environment.IsStaging())
{
    app.MapGet("/", () => "Hello from staging!");
}
else if (app.Environment.IsProduction())
{
    app.MapGet("/", () => "Hello from production!");
}
else/* if(app.Environment.IsEnvironment("alamakota"))*/
{
    app.MapGet("/", () => $"Hello from {app.Environment.EnvironmentName}!");
}



app.Run();

static void HelloApp(IApplicationBuilder helloApp)
{
        helloApp.Use(async (context, next) =>
        {
            Console.WriteLine("Begin of HelloUse");


            await next(context);


            Console.WriteLine("End of HelloUse");
        });

        helloApp.HelloRun();
}