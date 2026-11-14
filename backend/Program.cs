using backend.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// 1. Подключение контроллеров
builder.Services.AddControllers();

// 2. Подключение Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Регистрация нашего сервиса в DI (Dependency Injection)
// Теперь контроллеры смогут запрашивать IAppInfoService, не создавая его через new
builder.Services.AddSingleton<IAppInfoService, AppInfoService>();

// Регистрируем наш сервис как Singleton (один на всё приложение)
// Это нужно, чтобы память не сбрасывалась при каждом запросе
builder.Services.AddSingleton<TicketService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception != null)
        {
            // 1. Логируем понятную строку в консоль сервера
            Console.WriteLine($"!!! ОШИБКА СЕРВЕРА: {exception.Message} | Тип: {exception.GetType().Name}");

            // 2. Возвращаем клиенту безопасный ответ без стектрейса
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { 
                error = "Внутренняя ошибка сервера", 
                message = "Попробуйте позже" 
            });
        }
    });
});

// 4. Включаем Swagger только в режиме разработки (Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 5. Маршрутизация контроллеров
app.MapControllers();

// 6. Служебные эндпоинты (Minimal API)
// Проверка живости процесса (liveness)
app.MapGet("/health", () => Results.Ok(new { status = "ok", timestamp = DateTime.UtcNow }));

// Информация о приложении (используем внедренный сервис)
app.MapGet("/info", (IAppInfoService infoService) => 
{
    return Results.Ok(new
    {
        app = infoService.GetAppName(),
        environment = infoService.GetEnvironment(),
        version = infoService.GetVersion()
    });
});

app.Run();