using NHibernate;

namespace OhSoSecure.Core.DataAccess
{
    public interface ISessionSource
    {
        ISession CreateSession();
    }
}