using System.IO;
using System.Linq;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Orbital.Core
{
    public static class ServiceExtensions
    {
        public static void AddServicesByConvention(this IServiceCollection services)
        {
            var registrableTypes = IAssemblyScanner.Instance.Types.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                !x.IsNested &&
                x.IsAssignableTo<IService>()
            );

            foreach (var type in registrableTypes)
            {
                var matchingInterfaceType = type.GetInterface($"I{type.Name}");
                if (matchingInterfaceType == null) continue;

                if (matchingInterfaceType.IsGenericType && !matchingInterfaceType.IsGenericTypeDefinition)
                {
                    matchingInterfaceType = matchingInterfaceType.GetGenericTypeDefinition();
                }

                if (type.IsAssignableTo<ISingletonService>())
                {
                    services.AddSingleton(matchingInterfaceType, type);
                }
                else
                {
                    services.AddTransient(matchingInterfaceType, type);
                }
            }
        }

        public static void AddLog4Net(this IServiceCollection services, string logFileName)
        {
            services.AddLogging(builder =>
            {
                GlobalContext.Properties["LogDirectory"] = IAppSettings.Instance.PathToWorkingDirectory;
                GlobalContext.Properties["LogFileName"] = logFileName;

                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddConfiguration(IAppSettings.Instance.GetSection("Logging"));
                builder.AddLog4Net(Path.Combine(IAppSettings.Instance.PathToWorkingDirectory, "log4net.config"), true);
            });
        }
    }
}