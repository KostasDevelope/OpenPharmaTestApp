using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OpenPharmaTestApp.TaskListCustomers
{
    public interface ICustomerTaskListService : ITransientDependency
    {
        Task<CustomerTaskListDto> GetAsync(Guid customerId, Guid taskListId);
        Task<CustomerTaskListDto> CreateAsync(CreateCustomerTaskListDto model);
        Task<CustomerTaskListDto> DeleteAsync(Guid customerId, Guid taskListId);
    }
}
