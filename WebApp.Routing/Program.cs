var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "NULL");

    await next(context);
});

//jawne wywo�anie routingu powoduje, �e dzia�a on od miejsca wywo�ania, a nie od pocz�tku
app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "NULL");

    await next(context);
});


app.Map("/Bye", byeApp =>
{
    byeApp.UseRouting();

    byeApp.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/aaa", () => "Bye!");
    });

    byeApp.Run(async context =>
    {
        await context.Response.WriteAsync("...");
    });

});

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "Hello World!");
    endpoints.MapGet("/hello", () => "Hello!");
});*/

app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => "Hello!");


/*app.Run(async context =>
{
    await context.Response.WriteAsync("!!!");
});*/

//app.UseEndpoints....

app.Run();
