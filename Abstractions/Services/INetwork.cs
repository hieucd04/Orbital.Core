namespace Orbital.Core
{
    public interface INetwork : ISingletonService
    {
        private static INetwork _instance;

        static INetwork Instance => _instance ??= new Network();

        string LocalIpAddress { get; }
    }
}