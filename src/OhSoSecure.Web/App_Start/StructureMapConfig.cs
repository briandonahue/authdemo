using System.Web.Mvc;
using OhSoSecure.Core.IoC;
using StructureMap;

namespace OhSoSecure.Web.App_Start
{
    public class StructureMapConfig
    {
        public static void Configure()
        {
            StructureMapBootstrap.Initialize();

            DependencyResolver.SetResolver(new StructureMapMvcDependencyResolver(ObjectFactory.Container));
        }
    }
}