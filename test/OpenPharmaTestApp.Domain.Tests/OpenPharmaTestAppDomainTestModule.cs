using Volo.Abp.Modularity;

namespace OpenPharmaTestApp;

[DependsOn(
    typeof(OpenPharmaTestAppDomainModule),
    typeof(OpenPharmaTestAppTestBaseModule)
)]
public class OpenPharmaTestAppDomainTestModule : AbpModule
{

}
