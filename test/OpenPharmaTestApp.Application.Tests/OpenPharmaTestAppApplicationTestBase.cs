using Volo.Abp.Modularity;

namespace OpenPharmaTestApp;

public abstract class OpenPharmaTestAppApplicationTestBase<TStartupModule> : OpenPharmaTestAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
