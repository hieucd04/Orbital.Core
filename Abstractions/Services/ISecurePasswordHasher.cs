namespace Orbital.Core
{
    public interface ISecurePasswordHasher : ISingletonService
    {
        string Hash(string password);

        bool Verify(string password, string passwordHash);
    }
}