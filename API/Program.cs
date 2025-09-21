using Application;
using Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Включаем сериализацию свойств с private setters
        options.JsonSerializerOptions.IncludeFields = true;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

        // Разрешаем сериализацию всех свойств, включая с private setters
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info: new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Управление зоопарком API", Version = " Смирнов А.А. версия 1.15.137" });
});

// Добавляем application и infrastructure слои
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Настройка HTTP-запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Управление зоопарком API");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Управление зоопарком API успешно запущен! /swagger - просмотр документации API");

app.Run();