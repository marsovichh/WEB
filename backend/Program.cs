using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Подключение контроллеров
builder.Services.AddControllers();

// 2. Подключение Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Регистрация нашего сервиса в DI (Dependency Injection)
// Теперь контроллеры смогут запрашивать IAppInfoService, не создавая его через new
builder.Services.AddSingleton<IAppInfoService, AppInfoService>();

var app = builder.Build();

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