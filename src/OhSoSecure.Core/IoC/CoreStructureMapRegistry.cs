using StructureMap.Configuration.DSL;

namespace OhSoSecure.Core.IoC
{
    public class CoreStructureMapRegistry: Registry
    {
        public CoreStructureMapRegistry() {
            Scan(cfg =>
            {
                cfg.TheCallingAssembly();
                cfg.WithDefaultConventions();
            });
        }
    }
}