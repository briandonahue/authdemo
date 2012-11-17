using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.Security
{
    public interface IAuthService
    {
        bool Authenticate(string userName, string password);
        User CreateUser(string userName, string password, string firstName, string lastName);
        string GetLoginUrlFor(string userName);
    }
}