using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OpenPharmaTestApp.Data;

/* This is used if database provider does't define
 * IOpenPharmaTestAppDbSchemaMigrator implementation.
 */
public class NullOpenPharmaTestAppDbSchemaMigrator : IOpenPharmaTestAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
