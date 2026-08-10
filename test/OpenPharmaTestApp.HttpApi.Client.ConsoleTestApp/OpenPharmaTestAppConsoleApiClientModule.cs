using Microsoft.Extensions.DependencyInjection;
using OpenPharmaTestApp.EntityFrameworkCore;
using Polly;
using System;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace OpenPharmaTestApp.HttpApi.Client.ConsoleTestApp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(OpenPharmaTestAppHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule),
    typeof(OpenPharmaTestAppApplicationModule),
    typeof(OpenPharmaTestAppEntityFrameworkCoreModule)
    )]
public class OpenPharmaTestAppConsoleApiClientModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
            {
                clientBuilder.AddTransientHttpErrorPolicy(
                    policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                );
            });
        });
    }
}
