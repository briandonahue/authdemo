using System.Collections.Generic;
using Iesi.Collections.Generic;
using OhSoSecure.Core.Security;

namespace OhSoSecure.Core.Domain
{
    public class User
    {
        readonly Iesi.Collections.Generic.ISet<Role> roles = new HashedSet<Role>();

        protected User() {}
        public User(string userName, string password, string firstName, string lastName)
        {
            UserName = userName;
            Password = new HashedPassword(password);
            FirstName = firstName;
            LastName = lastName;
        }

        public virtual int Id { get; protected set; }
        public virtual string UserName { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }

        public virtual IEnumerable<Role> Roles
        {
            get { return roles; }
        }

        public virtual HashedPassword Password { get; protected set; }
    }
}