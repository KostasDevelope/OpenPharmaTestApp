using Volo.Abp.Modularity;

namespace OpenPharmaTestApp;

/* Inherit from this class for your domain layer tests. */
public abstract class OpenPharmaTestAppDomainTestBase<TStartupModule> : OpenPharmaTestAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
