using System.Linq;
using NHibernate;
using NHibernate.Linq;
using OhSoSecure.Core.Domain;
using OhSoSecure.Core.Security;

namespace OhSoSecure.Core.DataAccess
{
    public class UserRepository : IUserRepository {
        readonly ISession session;

        public UserRepository(ISession session)
        {
            this.session = session;
        }

        public User FindByUserName(string userName)
        {
            return session.Query<User>().FirstOrDefault(u => u.UserName == userName);
        }

        public void Save(User user)
        {
            session.Save(user);
        }

        public User CreateUser(string userName, string password, string firstName, string lastName)
        {
            var user = new User(userName, password, firstName, lastName);
            var role = session.Query<Role>().First(r => r.Name == AuthRole.Member.ToString());
            user.AssignRole(role);
            session.Save(user);
            return user;
        }
    }
}