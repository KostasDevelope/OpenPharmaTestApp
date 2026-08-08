using Volo.Abp.Modularity;

namespace OpenPharmaTestApp;

[DependsOn(
    typeof(OpenPharmaTestAppApplicationModule),
    typeof(OpenPharmaTestAppDomainTestModule)
)]
public class OpenPharmaTestAppApplicationTestModule : AbpModule
{

}
