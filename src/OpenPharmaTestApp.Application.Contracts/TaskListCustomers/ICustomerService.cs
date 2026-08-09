using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public interface ICustomerService : ITransientDependency
    {
        Task<PagedResultDto<CustomerDto>> SearchAsync(SearchInput searchInput, CancellationToken cancellationToken);
        Task<CustomerDto> GetAsync(Guid id);
        Task<CustomerDto> CreateAsync(CreateCustomerDto model);
        Task<CustomerDto> UpdateAsync(UpdateCustomerDto model);
        Task<CustomerDto> DeleteAsync(Guid id);
    }
}
