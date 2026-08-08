using OpenPharmaTestApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace OpenPharmaTestApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(OpenPharmaTestAppEntityFrameworkCoreModule),
    typeof(OpenPharmaTestAppApplicationContractsModule)
)]
public class OpenPharmaTestAppDbMigratorModule : AbpModule
{
}
