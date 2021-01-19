using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Orbital.Core
{
    internal class AppSettings : IAppSettings
    {
        readonly IConfigurationRoot _configurationRoot;

        public string PathToWorkingDirectory { get; }

        [UsedImplicitly]
        public AppSettings()
        {
            var pathToEntryAssembly = Assembly.GetEntryAssembly()?.Location;
            PathToWorkingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
                                     throw new DllNotFoundException("Entry assembly not found.");

            if (PathToWorkingDirectory == null)
                throw new DirectoryNotFoundException($"Could not obtain parent directory of: {pathToEntryAssembly}");

            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(PathToWorkingDirectory)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .Build();
        }

        public T Get<T>(string key) { return GetSection(key).Get<T>(); }

        public string Get(string key) => Get<string>(key);

        public IConfigurationSection GetSection(string key) => _configurationRoot.GetSection(key);
    }
}