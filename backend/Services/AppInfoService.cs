namespace backend.Services;

public class AppInfoService : IAppInfoService
{
    public string GetAppName() => "AtmNetwork API";
    public string GetEnvironment() => "Development";
    public string GetVersion() => "ЛР7";
}