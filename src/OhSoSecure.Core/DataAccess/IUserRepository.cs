using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.DataAccess
{
    public interface IUserRepository
    {
        User FindByUserName(string userName);
        void Save(User user);
        User CreateUser(string userName, string password, string firstName, string lastName);
    }
}