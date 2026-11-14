namespace backend.Services;

public interface IAppInfoService
{
    string GetAppName();
    string GetEnvironment();
    string GetVersion();
}