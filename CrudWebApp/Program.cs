using Bogus;
using Models;
using Services.Bogus.Fakes;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<ICrudService<User>, Services.Bogus.CrudService<User>>();
builder.Services.AddSingleton<ICrudService<User>, Services.Bogus.CrudService<User>>
    (x => new Services.Bogus.CrudService<User>(x.GetService<Faker<User>>()!, x.GetService<IConfiguration>()!.GetValue<int>("Bogus:Count")));
builder.Services.AddTransient<Faker<User>, UserFaker>(x => new UserFaker(x.GetService<IConfiguration>()!["Bogus:Locale"]!));

var app = builder.Build();

//minimal-api
app.MapGet("/", () => "Hello World!");
app.MapGet("/users", (ICrudService<User> service) => service.ReadAsync());
app.MapGet("/users/{id:int}", (ICrudService<User> service, int id) => service.ReadAsync(id));
app.MapDelete("/users/{id:int}", (ICrudService<User> service, int id) => service.DeleteAsync(id));
app.MapPost("/users", (ICrudService<User> service, User user) => service.CreateAsync(user));
app.MapPut("/users/{id:int}", (ICrudService<User> service, int id, User user) => service.UpdateAsync(id, user));

app.Run();
