using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenPharmaTestApp.Data;
using Volo.Abp.DependencyInjection;

namespace OpenPharmaTestApp.EntityFrameworkCore;

public class EntityFrameworkCoreOpenPharmaTestAppDbSchemaMigrator
    : IOpenPharmaTestAppDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreOpenPharmaTestAppDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the OpenPharmaTestAppDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<OpenPharmaTestAppDbContext>()
            .Database
            .MigrateAsync();
    }
}
