using System.Threading.Tasks;

namespace OpenPharmaTestApp.Data;

public interface IOpenPharmaTestAppDbSchemaMigrator
{
    Task MigrateAsync();
}
