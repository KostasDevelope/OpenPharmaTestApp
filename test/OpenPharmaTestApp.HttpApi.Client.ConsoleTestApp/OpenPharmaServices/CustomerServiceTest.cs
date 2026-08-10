using OpenPharmaTestApp.TaskListCustomers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OpenPharmaTestApp.HttpApi.Client.ConsoleTestApp.OpenPharmaServices
{
    public class CustomerServiceTest : ITransientDependency
    {
        private readonly ICustomerService _customerService;

        public CustomerServiceTest(
            ICustomerService customerService
            
            )
        {
            _customerService = customerService;
        }

        public async Task SearchAsync()
        {
            using var cts = new CancellationTokenSource();

            var modelDto = await _customerService.SearchAsync(new SearchInput
            {
                Filter = "",
                Sorting = "desc",
                MaxResultCount = 10,
                SkipCount = 0
            }, cts.Token);


            Console.WriteLine($"Total customer: {modelDto.TotalCount}");
            foreach (var identityUserDto in modelDto.Items)
            {
                Console.WriteLine($"Id: [{identityUserDto.Id}]; Name: {identityUserDto.Name}; Creation Time: {identityUserDto.CreationTime:dd.MM.yyyy HH:mm}");
            }
            cts.Cancel();
        }
    }
}
