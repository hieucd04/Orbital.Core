using Microsoft.Extensions.Configuration;

namespace Orbital.Core
{
    public interface IAppSettings : ISingletonService
    {
        private static IAppSettings _instance;

        static IAppSettings Instance => _instance ??= new AppSettings();

        string PathToWorkingDirectory { get; }

        T Get<T>(string key);

        string Get(string key);

        IConfigurationSection GetSection(string key);
    }
}