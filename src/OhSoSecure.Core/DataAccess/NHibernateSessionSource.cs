using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Cfg;
using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.DataAccess
{
    public class NHibernateSessionSource : ISessionSource
    {
        readonly Configuration _configuration;
        readonly object _factorySyncRoot = new object();
        readonly ISessionFactory _sessionFactory;

        public NHibernateSessionSource()
        {
            if (_sessionFactory != null) return;

            lock (_factorySyncRoot)
            {
                if (_sessionFactory != null) return;

                _configuration = AssembleConfiguration();
                _sessionFactory = _configuration.BuildSessionFactory();
            }
        }

        public ISession CreateSession()
        {
            return _sessionFactory.OpenSession();
        }

        public Configuration AssembleConfiguration()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("OhSoSecure")))
                .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<User>()
                                     .Conventions.Add(ForeignKey.EndsWith("Id"))
                )
                .BuildConfiguration();
        }
    }
}