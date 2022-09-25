using DebitManagement.API.Extendions;
using DebitManagement.Data.Interfaces;
using DebitManagement.Repository;
using DebitManagement.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("CoreSwagger", new OpenApiInfo()
    {
        Title = "Swagger on ASP.NET Core",
        Version = "1.0.0",
        Description = "Try Swagger on (ASP.NET Core 2.1)"
    });
});

builder.Services.AddDbContext<DebitContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddApplicationServices();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();