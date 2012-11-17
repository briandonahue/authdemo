using NHibernate;
using OhSoSecure.Core.DataAccess;
using StructureMap.Configuration.DSL;

namespace OhSoSecure.Core.IoC
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            ForSingletonOf<ISessionSource>().Use<NHibernateSessionSource>();

            For<ISession>().Use(c =>
            {
                var transaction = (NHibernateTransactionBoundary) c.GetInstance<ITransactionBoundary>();
                return transaction.CurrentSession;
            });

            For<ITransactionBoundary>().HybridHttpOrThreadLocalScoped().Use<NHibernateTransactionBoundary>();
        }
    }
}