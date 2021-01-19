using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Orbital.Core
{
    internal class AssemblyScanner : IAssemblyScanner
    {
        static readonly string[] Blacklist =
        {
            @"\node_modules\",
            @"\runtimes\",
            @"\x64\",
            "System",
            "Microsoft",
            "log4net",
            "JetBrains",
            "FluentValidation",
            "JavaScriptEngineSwitcher",
            "AdvancedStringBuilder",
            "ClearScript",
            "Newtonsoft",
            "React",
            "JSPool",
            "Pomelo",
            "Pluralize",
            "Confluent.Kafka",
            "MySqlConnector",
            "IdentityServer4",
            "IdentityModel"
        };

        #region Injected Services

        readonly IAppSettings _appSettings;

        #endregion

        public List<Type> Types { get; }

        List<Assembly> Assemblies { get; }

        [UsedImplicitly]
        public AssemblyScanner(IAppSettings appSettings)
        {
            _appSettings = appSettings;

            Types = new List<Type>();
            Assemblies = new List<Assembly>();

            Scan();
        }

        public void Scan()
        {
            Types.Clear();
            Assemblies.Clear();

            var assemblyFilePaths = Directory.GetFiles(_appSettings.PathToWorkingDirectory, "*.dll", SearchOption.AllDirectories)
                .Where(x => !Blacklisted(x));

            foreach (var assemblyFilePath in assemblyFilePaths)
            {
                try { Assemblies.Add(Assembly.LoadFrom(assemblyFilePath)); }
                catch (FileLoadException) { }
                catch (BadImageFormatException) { }
            }

            foreach (var assembly in Assemblies)
            {
                Types.AddRange(assembly.GetTypes().Where(type => !type.IsCompilerGenerated()));
            }
        }

        static bool Blacklisted(string filePath)
        {
            var blacklistRecords = Blacklist.GroupBy(
                    x => x.StartsWith(@"\") && x.EndsWith(@"\"),
                    (isFolder, paths) => (
                        IsFolder: isFolder,
                        Paths: paths.Select(x => isFolder ? @$"\{x}\" : x)
                    )
                )
                .ToArray();

            var blacklistFolders = string.Join('|', blacklistRecords.Single(x => x.IsFolder).Paths);
            var blacklistFiles = string.Join('|', blacklistRecords.Single(x => !x.IsFolder).Paths);

            return Regex.IsMatch(filePath, @$"^.+({blacklistFolders}).*\.dll$", RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(filePath, @$"^.+({blacklistFiles})(?!.*(\\|\/)).*\.dll$", RegexOptions.IgnoreCase);
        }
    }
}