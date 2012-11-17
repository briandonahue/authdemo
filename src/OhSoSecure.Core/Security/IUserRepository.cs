using System.Web.Security;
using NHibernate;
using NHibernate.Linq;
using OhSoSecure.Core.Domain;
using System.Linq;

namespace OhSoSecure.Core.Security
{
    public interface IUserRepository
    {
        User FindByUserName(string userName);
        void Save(User user);
    }

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
    }
}