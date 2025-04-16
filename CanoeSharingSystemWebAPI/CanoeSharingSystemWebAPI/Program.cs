using Microsoft.EntityFrameworkCore;
using CanoeSharingSystemWebAPI.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Setting up CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});



var key = builder.Configuration["Jwt:Key"];

var issuer = builder.Configuration["Jwt:Issuer"];

//https://www.infoworld.com/article/2336284/how-to-implement-jwt-authentication-in-aspnet-core.html
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyAccess", policy =>
        policy.RequireClaim("role", "user"));

    options.AddPolicy("StoreOnlyAccess", policy =>
        policy.RequireClaim("role", "store"));

    options.AddPolicy("UserOrStoreAccess", policy => policy.RequireAssertion(x => x.User.HasClaim(x => x.Type == "role" && (x.Value == "user" || x.Value == "store"))));
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CanoeSharingAPIDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CanoeSharingDB"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
