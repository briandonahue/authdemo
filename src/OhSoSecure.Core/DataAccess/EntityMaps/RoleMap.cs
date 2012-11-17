using FluentNHibernate.Mapping;
using OhSoSecure.Core.Domain;

namespace OhSoSecure.Core.DataAccess.EntityMaps
{
    public class RoleMap: ClassMap<Role>
    {

        public RoleMap() {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}