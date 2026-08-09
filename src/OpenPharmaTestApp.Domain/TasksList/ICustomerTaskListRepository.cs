using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OpenPharmaTestApp.TasksList
{
    public interface ICustomerTaskListRepository : IBasicRepository<CustomerTaskList>
    {
        Task<CustomerTaskList?> GetAsync(Guid customerId, Guid taskListId);
        Task<CustomerTaskList?> CreateAsync(CustomerTaskList taskList);
        Task DeleteAsync(CustomerTaskList taskList);
        Task<int> GetAssignTaskListCountAsync(Guid taskListId);
        Task<int> GetAssignCustomerCountAsync(Guid customerId);
    }
}
