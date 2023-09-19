var builder = WebApplication.CreateBuilder(args);

//konfiguracja service collection



var app = builder.Build();
//konfiguracja aplikacji



app.Use(async (context, next) =>
{
    Console.WriteLine("Begin of Use1");


    await next(context);


    Console.WriteLine("End of Use1");
});

app.Map("/hello", helloApp =>
{
    helloApp.Use(async (context, next) =>
    {
        Console.WriteLine("Begin of HelloUse");


        await next(context);


        Console.WriteLine("End of HelloUse");
    });

    helloApp.Run(async context =>
    {
        Console.WriteLine("Begin of HelloRun");
        await context.Response.WriteAsync("Hello o/");
        Console.WriteLine("End of HelloRun");
    });

});


app.Use(async (context, next) =>
{
    Console.WriteLine("Begin of Use2");


    await next(context);


    Console.WriteLine("End of Use2");
});

app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), nameApp =>
{
    nameApp.Run(async context =>
    {
        await context.Response.WriteAsync($"Witaj {context.Request.Query["name"]}");
    });

});

app.Run(async context =>
{
    Console.WriteLine("Begin of Run");
    await context.Response.WriteAsync("Under Construction");
    Console.WriteLine("End of Run");
});



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
