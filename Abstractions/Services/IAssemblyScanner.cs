using System;
using System.Collections.Generic;

namespace Orbital.Core
{
    public interface IAssemblyScanner : ISingletonService
    {
        private static IAssemblyScanner _instance;

        static IAssemblyScanner Instance => _instance ??= new AssemblyScanner(IAppSettings.Instance);

        List<Type> Types { get; }

        void Scan();
    }
}