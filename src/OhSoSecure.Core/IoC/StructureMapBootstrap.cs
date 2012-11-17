using StructureMap;

namespace OhSoSecure.Core.IoC
{
    public class StructureMapBootstrap
    {
        public static void Initialize()
        {
            ObjectFactory.Initialize(cfg => cfg.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.LookForRegistries();
            }));
        }
    }
}