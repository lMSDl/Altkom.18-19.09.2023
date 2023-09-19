using Bogus;
using Models;
using Services.Bogus.Fakes;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

byte[] signingKey = Guid.NewGuid().ToByteArray();  


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<ICrudService<User>, Services.Bogus.CrudService<User>>();
builder.Services.AddSingleton<ICrudService<User>, Services.Bogus.CrudService<User>>
    (x => new Services.Bogus.CrudService<User>(x.GetService<Faker<User>>()!, x.GetService<IConfiguration>()!.GetValue<int>("Bogus:Count")));
builder.Services.AddTransient<Faker<User>, UserFaker>(x => new UserFaker(x.GetService<IConfiguration>()!["Bogus:Locale"]!));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/";
    });


/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
        RequireExpirationTime = true
    };
});*/
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyForPaul", x => x.RequireUserName("Paul"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

//minimal-api
app.MapGet("/", () => "Hello World!");
app.MapGet("/users", [Authorize] (ICrudService<User> service) => service.ReadAsync());
app.MapGet("/users/{id:int}", [Authorize(Policy = "OnlyForPaul")] (ICrudService<User> service, int id) => service.ReadAsync(id));
app.MapDelete("/users/{id:int}", [Authorize] (ICrudService<User> service, int id) => service.DeleteAsync(id));
app.MapPost("/users", [Authorize(Roles = "Create")](ICrudService<User> service, User user) => service.CreateAsync(user));
app.MapPut("/users/{id:int}", (ICrudService<User> service, int id, User user) => service.UpdateAsync(id, user));



/*app.MapPost("/login", async (ICrudService<User> service, User user) =>
{
    user = (await service.ReadAsync()).Where(x => x.Username == user.Username).SingleOrDefault(x => x.Password == user.Password);

    if(user != null)
    {
        var claims = new List<Claim>()
        {
            new Claim("alamakota", "kotmaale"),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "Create"),
            new Claim(ClaimTypes.Role, "Delete")
        };



        var tokenDescriptor = new SecurityTokenDescriptor();
        tokenDescriptor.Expires = DateTime.Now.AddSeconds(30);
        tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);
        tokenDescriptor.Subject = new ClaimsIdentity(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    return null;
});*/

app.MapGet("/login", async context =>
{
    var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
    var claimPrincipal = new ClaimsPrincipal(claimIdentity);

    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
});

app.Run();
