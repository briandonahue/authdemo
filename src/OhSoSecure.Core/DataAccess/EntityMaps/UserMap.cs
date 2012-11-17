using FluentNHibernate.Mapping;
using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.DataAccess.EntityMaps
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.UserName);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Component(x => x.Password, m =>
            {
                m.Map(x => x.PasswordHash);
                m.Map(x => x.PasswordSalt);
            });
            HasManyToMany(x => x.Roles).AsSet().Access.CamelCaseField().Cascade.AllDeleteOrphan();
        }
    }
}