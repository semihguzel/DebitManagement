using System.Text.Json.Serialization;
using DebitManagement.API.Extensions;
using DebitManagement.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerServices();

builder.Services.AddDbContext<DebitContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("AppSettings:JwtKey").Value);

builder.Services.AddApplicationServices();

builder.Services.AddMapperProfiles();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core"); });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();