using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenPharmaTestApp.HttpApi.Client.ConsoleTestApp.OpenPharmaServices;
using OpenPharmaTestApp.TaskListCustomers;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace OpenPharmaTestApp.HttpApi.Client.ConsoleTestApp;

class Program
{
    static async Task Main(string[] args)
    {
        using (var application = await AbpApplicationFactory.CreateAsync<OpenPharmaTestAppConsoleApiClientModule>(options =>
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            builder.AddJsonFile("appsettings.secrets.json", true);
            options.Services.ReplaceConfiguration(builder.Build());
            options.UseAutofac();
        }))
        {
            await application.InitializeAsync();

            
            var сustomerServiceTest = application.ServiceProvider.GetRequiredService<CustomerServiceTest>();
            await сustomerServiceTest.SearchAsync();

            Console.WriteLine("Press ENTER to stop application...");
            Console.ReadLine();

            await application.ShutdownAsync();
        }
    }
}
