using Microsoft.EntityFrameworkCore;
using CanoeSharingSystemWebAPI.Entities;

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

app.UseAuthorization();

app.MapControllers();

app.Run();
