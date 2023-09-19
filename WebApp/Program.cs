var builder = WebApplication.CreateBuilder(args);

//konfiguracja service collection



var app = builder.Build();
//konfiguracja aplikacji


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
